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
            var accounts = _context.Accounts.ToList();

            foreach (var account in accounts)
            {
            }

            _context.SaveChanges();
            return NoContent();
        }
    }
}
