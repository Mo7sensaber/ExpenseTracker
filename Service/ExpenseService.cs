using AutoMapper;
using Domain.Model;
using Domain.RepoInterface;
using Service.Specifications;
using ServiceAbestraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public ExpenseService(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<ReturnExpenceDto> AddExpense(ReturnExpenceDto expense)
        {
            var expenseRepo = mapper.Map<Expense>(expense);
            await unit.GetRepository<Expense, int>().AddAsync(expenseRepo);
            await unit.SaveChangesAsync();
            return expense;
        }

        public async Task DeleteExpense(ReturnExpenceDto expense)
        {
            var Del = mapper.Map<Expense>(expense);
            var IsExist = await unit.GetRepository<Expense, int>().GetByIdAsync(Del.Id);
            if (IsExist == null)
            {
                throw new Exception($"NotFound Expense with Id {expense.Id}");
            }
            await unit.GetRepository<Expense, int>().Delete(Del.Id);
            await unit.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReturnExpenceDto>> GetAllExpenses(int? month,int? year)
        {
            var Spec = new AddDateToExpense(month, year);
            var expenses = await unit.GetRepository<Expense, int>().GetAllWithIncludeAsync(Spec);
            return mapper.Map<IEnumerable<ReturnExpenceDto>>(expenses);
        }

        public async Task<ReturnExpenceDto> GetExpenseById(int id)
        {
            var expense = await unit.GetRepository<Expense, int>().GetByIdAsync(id);
            if (expense == null)
            {
                throw new Exception($"NotFound Expense with Id {id}");
            }
            return mapper.Map<ReturnExpenceDto>(expense);
        }

        public async Task UpdateExpense(ReturnExpenceDto expense)
        {
            var IsExist = await unit.GetRepository<Expense, int>()
                                   .GetByIdAsync(expense.Id);

            if (IsExist == null)
                throw new Exception($"NotFound Expense with Id {expense.Id}");

            if (expense.Amount != 0)
                IsExist.Amount = expense.Amount;

            if (expense.Date != default)
                IsExist.Date = expense.Date;

            if (expense.Description!="string")
                IsExist.Description = expense.Description;

            if (expense.CategoryId != 0)
                IsExist.CategoryId = expense.CategoryId;

            await unit.GetRepository<Expense, int>().Update(IsExist);
            await unit.SaveChangesAsync();
        }
    }
}
