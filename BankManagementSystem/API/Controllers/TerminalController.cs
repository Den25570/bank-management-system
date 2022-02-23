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
using API.Models;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TerminalController : ControllerBase
    {
        private readonly BankManagmentSystemContext _context;

        public TerminalController(BankManagmentSystemContext context)
        {
            _context = context;
        }

        // POST: api/terminal/auth
        [HttpPost("auth")]
        public ActionResult<Response> AuthorizeAccount([FromBody] AuthorizeCard authorizeCard)
        {
            var code = int.Parse(authorizeCard.Number.Substring(0, 4));
            var individualNumber = int.Parse(authorizeCard.Number.Substring(4));
            var account = _context.Accounts.Where(a => a.AccountTypeId == code && a.IndividualNumber == individualNumber).FirstOrDefault();

            if (account == null) return new Response("Карта не найдена", false, false);
            if (account.PIN != authorizeCard.PIN) return new Response("PIN введён неправильно", false, false);

            return new Response("Карта авторизована", true, account);
        }

        [HttpPost("pay")]
        public ActionResult<Response> Pay([FromBody] TerminalPayment payment)
        {
            var account = _context.Accounts.Find(payment.AccountId);
            
            if (account.Debit + account.Credit + account.Balance - payment.Sum <= 0) return new Response("Недостаточно баланса", false, false);

            account.Credit -= payment.Sum;

            _context.SaveChanges();

            return new Response("Оплата осуществена успешно", true, account);
        }
    }
}
