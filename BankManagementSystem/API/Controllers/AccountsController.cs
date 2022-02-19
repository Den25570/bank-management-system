#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Database;
using API.Models.Requests;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BankManagmentSystemContext _context;

        public AccountsController(BankManagmentSystemContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccounts()
        {
            return _context.Accounts.ToList();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult EndDay([FromBody] EndOfBankDay endOfBankDay)
        {
            var accounts = _context.Accounts
                .ToList();
            var devAccounts = _context.Accounts
                .Where(a => a.AccountTypeId == 7327)
                .ToList();
            var deposits = _context.Deposits
                .Where(d => d.Status)
                .Include(d => d.PercentAccount).ToList();

            foreach (var deposit in deposits)
            {
                deposit.DaysPassed += endOfBankDay.daysToPass;
                var currentDate = deposit.StartDate.AddDays(deposit.DaysPassed);
                var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

                var devAccount = devAccounts.Where(a => a.CurrencyId == deposit.CurrencyId).FirstOrDefault();
                if (deposit.DepositTypeId == 1)
                {
                    if ((deposit.EndDate - currentDate).Days <= 0)
                    {
                        AddPercentsToDeposit(deposit, devAccount, deposit.LastPercentEvaluationDate, deposit.EndDate);
                        deposit.Status = false;
                    }
                    else if (deposit.LastPercentEvaluationDate < firstDayOfMonth && currentDate >= firstDayOfMonth)
                    {
                        AddPercentsToDeposit(deposit, devAccount, deposit.LastPercentEvaluationDate, firstDayOfMonth);
                    }
                }
                if (deposit.DepositTypeId == 2)
                {
                    if ((deposit.EndDate - currentDate).Days <= 0)
                    {
                        AddPercentsToDeposit(deposit, devAccount, deposit.StartDate, deposit.EndDate);
                        deposit.Status = false;
                    }
                }
            }

            foreach (var account in accounts)
            {
                account.Balance += account.Debit + account.Credit;
                account.Debit = 0;
                account.Credit = 0;
            }

            _context.SaveChanges();
            return CreatedAtAction("EndDay", null, "День Завершён");
        }

        private static void AddPercentsToDeposit(Deposit deposit, Account devAccount, DateTime startDate, DateTime endDate)
        {
            var deltaDays = (endDate - startDate).Days;
            var percentValue = (deltaDays / 365M) * (deposit.DepositPercent / 100M) * deposit.DepositAmount;

            // Зачисление процентов
            devAccount.Debit -= percentValue;
            deposit.PercentAccount.Credit += percentValue;
            deposit.LastPercentEvaluationDate = DateTime.Now;
        }
    }
}
