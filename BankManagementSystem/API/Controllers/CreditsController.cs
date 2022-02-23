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
    [ApiController]
    [Route("[controller]")]
    public class CreditsController : ControllerBase
    {
        private readonly BankManagmentSystemContext _context;

        public CreditsController(BankManagmentSystemContext context)
        {
            _context = context;
        }

        // GET: api/Credits
        [HttpGet]
        public ActionResult<IEnumerable<Credit>> GetCredits()
        {
            return _context.Credits
                .Include(d => d.MainAccount)
                .Include(d => d.PercentAccount)
                .Include(d => d.Client)
                .ToList();
        }

        // GET: api/Credits/5
        [HttpGet("{id}")]
        public ActionResult<Credit> GetCredit(int id)
        {
            var credit = _context.Credits
                .Include(d => d.MainAccount)
                .Include(d => d.PercentAccount)
                .Include(d => d.Client)
                .FirstOrDefault(d => d.Id == id);

            if (credit == null)
            {
                return NotFound();
            }

            return credit;
        }

        [HttpGet("codes")]
        public ActionResult<IEnumerable<AccountType>> GetCreditCodes(int id)
        {
            var creditCodes = _context.AccountTypes
                .Where(t => t.AccountSubclassId == 241 || t.AccountSubclassId == 242)
                .ToList();

            return creditCodes;
        }

        [HttpPost("Revoke/{id}")]
        public IActionResult RevokeCredit(int id)
        {
            var credit = _context.Credits
                .Include(d => d.MainAccount)
                .Include(d => d.PercentAccount)
                .FirstOrDefault(d => d.Id == id);

            var cashAccount = _context.Accounts.FirstOrDefault(a => a.AccountTypeId == 1010 && a.CurrencyId == credit.CurrencyId);
            var devAccount = _context.Accounts.FirstOrDefault(a => a.AccountTypeId == 7327 && a.CurrencyId == credit.CurrencyId);

            if (credit == null)
                return NotFound();
            if (!credit.PrematureRepayment)
                return ValidationProblem("Нельзя заранее закрыть этот кредит");

            var currentDate = credit.StartDate.AddDays(credit.DaysPassed);
            var percentValue = credit.GetPercents(currentDate);

            credit.Status = false;
            //Окончание кредита
            devAccount.Credit += credit.CreditAmount;
            credit.MainAccount.Credit -= credit.CreditAmount;
            // Зачисление процентов
            devAccount.Credit += percentValue;
            credit.PercentAccount.Credit -= percentValue;
            credit.LastPercentEvaluationDate = currentDate;

            _context.SaveChanges();
            return CreatedAtAction("RevokeCredit", new { id = credit.Id }, "Депозит отозван");
        }

        [HttpPost]
        public ActionResult<Credit> CreateCredit(RegisterCredit credit)
        {
            var client = _context.Clients.Find(credit.ClientId);
            if (client == null)
                return ValidationProblem($"Incorrect client id = {credit.ClientId}");

            var cashAccount = _context.Accounts.FirstOrDefault(a => a.AccountTypeId == 1010 && a.CurrencyId == credit.CurrencyId);
            var devAccount = _context.Accounts.FirstOrDefault(a => a.AccountTypeId == 7327 && a.CurrencyId == credit.CurrencyId);

            var mainAccountCode = credit.CreditCode.GetCreditMainAccountCode();
            var percentAccountCode = credit.CreditCode.GetCreditPercentAccountCode();
            var existingCreditAccounts = _context.Accounts.Where(a => a.AccountTypeId == mainAccountCode).OrderByDescending(d => d.IndividualNumber).Take(1).ToList();
            var existingCreditPercentAccounts = _context.Accounts.Where(a => a.AccountTypeId == percentAccountCode).OrderByDescending(d => d.IndividualNumber).Take(1).ToList();
            
            var creditMainNumber = 1;
            if (existingCreditAccounts != null && existingCreditAccounts.Count == 1) creditMainNumber = existingCreditAccounts[0].IndividualNumber +1;
            var creditPercentNumber = 1;
            if (existingCreditAccounts != null && existingCreditAccounts.Count == 1) creditPercentNumber = existingCreditAccounts[0].IndividualNumber + 1;

            var creditModel = new Credit()
            {
                ClientId = credit.ClientId,
                ContractTerm = credit.ContractTerm,
                CurrencyId = credit.CurrencyId,
                CreditAmount = credit.CreditAmount,
                CreditTypeId = credit.CreditTypeId,
                StartDate = credit.StartDate,
                EndDate = credit.EndDate,
                CreditPercent = credit.Percent,
                PrematureRepayment = credit.PrematureRepayment,
                CreditNumber = creditMainNumber + creditPercentNumber,
                Status = true,
                LastPercentEvaluationDate = credit.StartDate,
                MainAccount = new Account()
                {
                    AccountActivityId = 1,
                    AccountTypeId = mainAccountCode,
                    PIN = mainAccountCode.ReverseInt(),
                    Balance = 0,
                    Credit = 0,
                    Debit = 0,
                    IndividualNumber = creditMainNumber,
                    Name = $"Кредит {client.Firstname} {client.Lastname} {client.Middlename}",
                    OwnerId = client.Id,
                    CurrencyId=credit.CurrencyId,
                },
                PercentAccount = new Account()
                {
                    AccountActivityId = 1,
                    AccountTypeId = percentAccountCode,
                    PIN = percentAccountCode.ReverseInt(),
                    Balance = 0,
                    Credit = 0,
                    Debit = 0,
                    IndividualNumber = creditPercentNumber,
                    Name = $"Проценты по кредиту {client.Firstname} {client.Lastname} {client.Middlename}",
                    OwnerId = client.Id,
                    CurrencyId = credit.CurrencyId,
                }
            };

            // Перевод денег на счёт
            devAccount.Debit -= credit.CreditAmount;
            creditModel.MainAccount.Debit += credit.CreditAmount;

            _context.Credits.Add(creditModel);
            _context.SaveChanges();

            return CreatedAtAction("CreateCredit", new { id = creditModel.Id }, "Кредит создан");
        }

        // DELETE: api/Credits/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCredit(int id)
        {
            var credit = _context.Credits.Find(id);
            if (credit == null)
            {
                return NotFound();
            }

            _context.Credits.Remove(credit);
            _context.SaveChanges();

            return NoContent();
        }

        private bool CreditExists(int id)
        {
            return _context.Credits.Any(e => e.Id == id);
        }
    }
}
