using Domain.Model;
using Domain.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class Specification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseClass<Tkey>
    {
        public Specification(Expression<Func<TEntity, bool>> creteria)
        {
            Creteria = creteria;
        }
        public Expression<Func<TEntity, bool>> Creteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        
    }
}
