using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepoInterface
{
    public interface IUnitOfWork
    {
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseClass<TKey>;
         Task<int> SaveChangesAsync();
    }
}
