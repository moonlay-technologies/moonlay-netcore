using Core.Domain.Repositories;
using System.Linq;

namespace Core.Domain.Repositories
{
    public interface IEntityRepository<TEntity> : IRepositoryBase<TEntity>
    {
        IQueryable<TEntity> Query { get; }
    }
}