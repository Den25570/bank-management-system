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
using API.Extensions;

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
            var credits = _context.Credits
                .Where(d => d.Status)
                .Include(d => d.MainAccount)
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
                        AddPercentsToDeposit(deposit, devAccount, deposit.EndDate);
                        deposit.Status = false;
                        devAccount.Debit -= deposit.DepositAmount;
                        deposit.MainAccount.Credit += deposit.DepositAmount;
                    }
                    else if (deposit.LastPercentEvaluationDate < firstDayOfMonth && currentDate >= firstDayOfMonth)
                    {
                        AddPercentsToDeposit(deposit, devAccount, firstDayOfMonth);
                    }
                }
                if (deposit.DepositTypeId == 2)
                {
                    if ((deposit.EndDate - currentDate).Days <= 0)
                    {
                        AddPercentsToDeposit(deposit, devAccount, deposit.EndDate);
                        deposit.Status = false;
                        devAccount.Debit -= deposit.DepositAmount;
                        deposit.MainAccount.Credit += deposit.DepositAmount;
                    }
                }
            }

            foreach (var credit in credits)
            {
                credit.DaysPassed += endOfBankDay.daysToPass;
                var currentDate = credit.StartDate.AddDays(credit.DaysPassed);
                var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

                var devAccount = devAccounts.Where(a => a.CurrencyId == credit.CurrencyId).FirstOrDefault();
                if ((credit.EndDate - currentDate).Days <= 0)
                {
                    AddPercentsToCredit(credit, devAccount, credit.EndDate);
                    credit.Status = false;
                    devAccount.Credit += (credit.CreditAmount - credit.PayedToDate);
                    credit.MainAccount.Credit -= (credit.CreditAmount - credit.PayedToDate);
                    credit.PayedToDate = credit.CreditAmount;
                }
                else if (credit.LastPercentEvaluationDate < firstDayOfMonth && currentDate >= firstDayOfMonth)
                {
                    AddPercentsToCredit(credit, devAccount, firstDayOfMonth);
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

        private static void AddPercentsToDeposit(Deposit deposit, Account devAccount, DateTime date)
        {
            var percentValue = deposit.GetPercents(date);
            devAccount.Debit -= percentValue;
            deposit.PercentAccount.Credit += percentValue;
            deposit.LastPercentEvaluationDate = date;
        }

        private static void AddPercentsToCredit(Credit credit, Account devAccount, DateTime date)
        {
            var percentValue = credit.GetPercents(date);
            devAccount.Credit += percentValue;
            credit.PercentAccount.Credit -= percentValue;
            
            var mainValue = credit.GetMainPaymentValue(date);
            devAccount.Credit += mainValue;
            credit.MainAccount.Credit -= mainValue;
            credit.PayedToDate += mainValue;

            credit.LastPercentEvaluationDate = date;
        }
    }
}
