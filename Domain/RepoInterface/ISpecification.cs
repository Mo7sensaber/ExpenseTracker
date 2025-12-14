using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepoInterface
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseClass<TKey>
    {
        public Expression<Func<TEntity,bool>> Creteria { get; }
        public List<Expression<Func<TEntity, object>>> Includes { get; }

    }
}
