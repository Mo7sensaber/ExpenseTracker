using Microsoft.AspNetCore.Mvc;
using ServiceAbestraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController:ControllerBase
    {
        private readonly IManagerService manager;

        public ExpensesController(IManagerService manager)
        {
            this.manager = manager;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnExpenceDto>> GetExpenseById(int id)
        {
            var expense =await manager.expence.GetExpenseById(id);
            return Ok(expense);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnExpenceDto>>> GetAllExpenses(int? month,int? year)
        {
            var expences=await manager.expence.GetAllExpenses(month,year);
            return Ok(expences);
        }
        [HttpPost]
        public async Task<ActionResult<ReturnExpenceDto>> AddExpence(ReturnExpenceDto expence)
        {
            var addExpence=await manager.expence.AddExpense(expence);
            return Ok(addExpence);
        }
        [HttpPut]
        public async Task<ActionResult<ReturnExpenceDto>> UpdateExpence(ReturnExpenceDto expence)
        {
            await manager.expence.UpdateExpense(expence);
            return Ok(expence);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpence(int id)
        {
            var FindExpence = await manager.expence.GetExpenseById(id);
            await manager.expence.DeleteExpense(FindExpence);
            return Ok("Expense Deleted");
        }
    }
}
