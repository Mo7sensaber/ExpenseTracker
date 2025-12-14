using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbestraction
{
    public interface IReportService
    {
        Task<IEnumerable<ReturnTotalExpenseDto>> GetTotalExpenses();
        Task<HighiestAndLowiestAmountDto> GetHighiestAndLowiestAmount(int? month, int? year);
    }
}
