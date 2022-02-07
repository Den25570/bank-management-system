﻿#nullable disable
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
    public class DepositsController : ControllerBase
    {
        private readonly BankManagmentSystemContext _context;

        public DepositsController(BankManagmentSystemContext context)
        {
            _context = context;
        }

        // GET: api/Deposits
        [HttpGet]
        public ActionResult<IEnumerable<Deposit>> GetDeposits()
        {
            return _context.Deposits
                .Include(d => d.MainAccount)
                .Include(d => d.PercentAccount)
                .Include(d => d.Client)
                .ToList();
        }

        // GET: api/Deposits/5
        [HttpGet("{id}")]
        public ActionResult<Deposit> GetDeposit(int id)
        {
            var deposit = _context.Deposits
                .Include(d => d.MainAccount)
                .Include(d => d.PercentAccount)
                .Include(d => d.Client)
                .FirstOrDefault(d => d.Id == id);

            if (deposit == null)
            {
                return NotFound();
            }

            return deposit;
        }

        [HttpPost("Revoke/{id}")]
        public IActionResult RevokeDeposit(int id)
        {
            var deposit = _context.Deposits.Find(id);
            if (deposit == null)
            {
                return NotFound();
            }

            _context.Deposits.Remove(deposit);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Deposit> CreateDeposit(RegisterDeposit deposit)
        {
            var client = _context.Clients.Find(deposit.ClientId);
            if (client == null)
                return ValidationProblem($"Incorrect client id = {deposit.ClientId}");

            var mainAccountCode = deposit.DepositTypeId.GetMainAccountCode();
            var percentAccountCode = deposit.DepositTypeId.GetPercentAccountCode();
            var existingDepositAccounts = _context.Accounts.Where(a => a.AccountTypeId == mainAccountCode).OrderByDescending(d => d.IndividualNumber).Take(1).ToList();
            var existingDepositPercentAccounts = _context.Accounts.Where(a => a.AccountTypeId == percentAccountCode).OrderByDescending(d => d.IndividualNumber).Take(1).ToList();
            
            var depositMainNumber = 1;
            if (existingDepositAccounts != null && existingDepositAccounts.Count == 1)
                depositMainNumber = existingDepositAccounts[0].IndividualNumber +1;
            var depositPercentNumber = 1;
            if (existingDepositAccounts != null && existingDepositAccounts.Count == 1)
                depositPercentNumber = existingDepositAccounts[0].IndividualNumber + 1;

            var depositModel = new Deposit()
            {
                ClientId = deposit.ClientId,
                ContractTerm = deposit.ContractTerm,
                CurrencyId = deposit.CurrencyId,
                DepositAmount = deposit.DepositAmount,
                DepositTypeId = deposit.DepositTypeId,
                StartDate = deposit.StartDate,
                EndDate = deposit.EndDate,
                DepositNumber = depositMainNumber + depositPercentNumber,
                Status = true,
                MainAccount = new Account()
                {
                    AccountActivityId = 2,
                    AccountTypeId = mainAccountCode,
                    Balance = 0,
                    Credit = deposit.DepositAmount,
                    Debit = 0,
                    IndividualNumber = depositMainNumber,
                    Name = $"{client.Firstname} {client.Lastname} {client.Middlename}",
                    OwnerId = client.Id,
                },
                PercentAccount = new Account()
                {
                    AccountActivityId = 2,
                    AccountTypeId = percentAccountCode,
                    Balance = 0,
                    Credit = 0,
                    Debit = 0,
                    IndividualNumber = depositPercentNumber,
                    Name = $"{client.Firstname} {client.Lastname} {client.Middlename}",
                    OwnerId = client.Id,
                }
            };
            _context.Deposits.Add(depositModel);
            _context.SaveChanges();

            return CreatedAtAction("CreateDeposit", new { id = depositModel.Id }, depositModel.Id);
        }

        // DELETE: api/Deposits/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDeposit(int id)
        {
            var deposit = _context.Deposits.Find(id);
            if (deposit == null)
            {
                return NotFound();
            }

            _context.Deposits.Remove(deposit);
            _context.SaveChanges();

            return NoContent();
        }

        private bool DepositExists(int id)
        {
            return _context.Deposits.Any(e => e.Id == id);
        }
    }
}