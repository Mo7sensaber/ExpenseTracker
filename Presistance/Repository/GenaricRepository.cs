using Domain.Model;
using Domain.RepoInterface;
using Microsoft.EntityFrameworkCore;
using Peresistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistance.Repository
{
    public class GenaricRepository<TEntity, Tkey> : IGenaricRepository<TEntity, Tkey> where TEntity : BaseClass<Tkey>
    {
        private readonly ExpenceContext context;

        public GenaricRepository(ExpenceContext context) 
        {
            this.context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
             await context.Set<TEntity>().AddAsync(entity);
        }

        public async Task Delete(Tkey id)
        {
            var entity =await context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                 context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync(); 
        }

        public async Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(ISpecification<TEntity, Tkey> spec)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), spec).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Tkey id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task Update(TEntity entity)
        {
            var existEntity = await context.Set<TEntity>().FindAsync(entity.Id);
            if (existEntity != null)
            {
                context.Set<TEntity>().Update(entity);
            }
        }
    }
}
