using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class AddDateToExpense : Specification<Expense, int>
    {
        public AddDateToExpense(int? month,int? year) : base(e=>(e.Date.Month==month&&e.Date.Year==year)
            ||(!month.HasValue && e.Date.Year == year)||(e.Date.Month==month&&!year.HasValue)
        ||(!month.HasValue&&!year.HasValue))
        {
            AddInclude(e => e.Category);
        }

    }
}
