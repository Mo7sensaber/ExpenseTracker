using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepoInterface
{
    public interface IGenaricRepository<TEntity,Tkey> where TEntity : BaseClass<Tkey>
    {
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Tkey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(ISpecification<TEntity, Tkey> spec);

    }
}
