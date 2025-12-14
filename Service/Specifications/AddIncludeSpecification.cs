using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class AddIncludeSpecification : Specification<Expense, int>
    {
        public AddIncludeSpecification() : base(null)
        {
            AddInclude(e => e.Category);
        }
    }
}
