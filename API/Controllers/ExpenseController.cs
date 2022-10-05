using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Expense>>> GetExpenses()
        {
            var expenses = await _context.Expenses.ToListAsync();

            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpenses(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        [HttpGet("total_amount")]
        public async Task<ActionResult<Expense>> TotalExpenses()
        {
            var totalAmount = await _context.Expenses.SumAsync(e => e.Amount);

            return Ok(totalAmount);
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
        {

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return Ok("Sucessfully Added!");
        }

        [HttpPut]
        public async Task<ActionResult<Expense>> EditExpense(Expense expense)
        {

            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();

            return Ok("Sucessfully Edited!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)
        {
            var expenseObj = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expenseObj);
            await _context.SaveChangesAsync();

            return Ok("Sucessfully Deleted!");
        }
    }

}