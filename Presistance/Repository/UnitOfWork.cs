using Domain.Model;
using Domain.RepoInterface;
using Peresistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistance.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExpenceContext context;

        public UnitOfWork(ExpenceContext context) {
            this.context = context;
        }
        private readonly Dictionary<string, object> repositories = new Dictionary<string, object>();
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseClass<TKey>
        {
            var type = typeof(TEntity).Name;
            if (repositories.ContainsKey(type))
            {
                return (IGenaricRepository<TEntity, TKey>)repositories[type];
            }
            var repository = new GenaricRepository<TEntity, TKey>(context);
            repositories.Add(type, repository);
            return repository;
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
