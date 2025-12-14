using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbestraction
{
    public interface IExpenseService
    {
        Task<ReturnExpenceDto> GetExpenseById(int id);
        Task<IEnumerable<ReturnExpenceDto>> GetAllExpenses(int? month,int? year);
        Task<ReturnExpenceDto> AddExpense(ReturnExpenceDto expense);
        Task UpdateExpense(ReturnExpenceDto expense);
        Task DeleteExpense(ReturnExpenceDto expense);
    }
}
