#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Database;

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
        public ActionResult EndDay()
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
                var devAccount = devAccounts.Where(a => a.CurrencyId == deposit.CurrencyId).FirstOrDefault();
                if (deposit.DepositTypeId == 1)
                {
                    if (deposit.EndDate.Year == DateTime.Now.Year && deposit.EndDate.DayOfYear == DateTime.Now.DayOfYear)
                    {
                        AddPercentsToDeposit(deposit, devAccount, deposit.LastPercentEvaluationDate, deposit.EndDate);
                        deposit.Status = false;
                    }
                    else if (DateTime.Now.Day == 1)
                    {
                        AddPercentsToDeposit(deposit, devAccount, deposit.LastPercentEvaluationDate, DateTime.Now);
                    }
                }
                if (deposit.DepositTypeId == 2)
                {
                    if (deposit.EndDate.Year == DateTime.Now.Year && deposit.EndDate.DayOfYear == DateTime.Now.DayOfYear)
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
            var percentValue = (deltaDays / 365) * deposit.DepositPercent * deposit.DepositAmount;

            // Зачисление процентов
            devAccount.Debit -= percentValue;
            deposit.PercentAccount.Credit += percentValue;
            deposit.LastPercentEvaluationDate = DateTime.Now;
        }
    }
}
