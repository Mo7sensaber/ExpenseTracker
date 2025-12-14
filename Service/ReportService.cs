using AutoMapper;
using Domain.Model;
using Domain.RepoInterface;
using Microsoft.EntityFrameworkCore.Query;
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
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public ReportService(IUnitOfWork unit , IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public async Task<HighiestAndLowiestAmountDto> GetHighiestAndLowiestAmount(int? month, int? year)
        {
            var spec = new AddDateToExpense(month,year);

            var result = await unit.GetRepository<Expense, int>()
                                   .GetAllWithIncludeAsync(spec);

            var query = result.AsQueryable();

            //// فلترة بالشهر والسنة لو اتبعتوا
            //if (month.HasValue)
            //    query = query.Where(e => e.Date.Month == month.Value);

            //if (year.HasValue)
            //    query = query.Where(e => e.Date.Year == year.Value);

            //if (!query.Any())
            //    return new HighiestAndLowiestAmountDto
            //    {
            //        HighiestCategory = null,
            //        LowiestCategory = null,
            //        Total = 0
            //    };

            var highest = query
                .OrderByDescending(e => e.Amount)
                .FirstOrDefault()?.Category?.Name;

            var lowest = query
                .OrderBy(e => e.Amount)
                .FirstOrDefault()?.Category?.Name;

            var totalExpense = query.Sum(e => e.Amount);

            return new HighiestAndLowiestAmountDto
            {
                HighiestCategory = highest,
                LowiestCategory = lowest,
                Total = totalExpense
            };
        }

        public async Task<IEnumerable<ReturnTotalExpenseDto>> GetTotalExpenses()
        {
            var spec = new AddIncludeSpecification(); // هنا مش محتاج فلتر على الاسم

            var result = await unit.GetRepository<Expense, int>()
                                   .GetAllWithIncludeAsync(spec);

            var Spec = result
                .GroupBy(e => e.Category.Name)
                .Select(g => new ReturnTotalExpenseDto
                {
                    CategoryName = g.Key,
                    totalExpense = g.Sum(e => e.Amount)
                })
                .OrderBy(e => e.totalExpense)
                .ToList();

            return Spec; // مش محتاج AutoMapper هنا
        }


    }
}
