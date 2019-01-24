using ExtCore.Data.Abstractions;
using System.Threading.Tasks;

namespace Core.Domain.Repositories
{
    public interface IRepositoryBase<TEntity> : IRepository
    {
        Task Update(TEntity entity);
    }
}