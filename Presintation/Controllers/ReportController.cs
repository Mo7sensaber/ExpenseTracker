using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IManagerService manager;

        public ReportController(IManagerService manager)
        {
            this.manager = manager;
        }
        [HttpGet("MonthlyExpenseReport")]
        public async Task<ActionResult<HighiestAndLowiestAmountDto>> GetMonthlyExpenseReport(int? month, int? year)
        {
            var report= await manager.report.GetHighiestAndLowiestAmount(month, year);
            return Ok(report);
        }
        [HttpGet("TotalExpensesPerCategory")]
        public async Task<ActionResult<IEnumerable<ReturnTotalExpenseDto>>> GetTotalExpensesPerCategory()
        {
            var reports =await manager.report.GetTotalExpenses();
            return Ok(reports);
        }
    }
}
