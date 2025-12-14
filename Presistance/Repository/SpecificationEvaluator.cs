using Domain.Model;
using Domain.RepoInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistance.Repository
{
    public class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecification<TEntity, Tkey> specification) where TEntity : BaseClass<Tkey>
        {
            var query = inputQuery;
            if (specification.Creteria is not null)
            {
                query = query.Where(specification.Creteria);
            }



            if (specification.Includes is not null && specification.Includes.Count > 0)
            {
                foreach (var Exp in query)
                {
                    query = specification.Includes.Aggregate(query, (CurrentQuery, Expression) => CurrentQuery.Include(Expression));
                }
            }

            return query;
        }
    }
}
