using ExtCore.Data.EntityFramework;
using Core.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Domain.Repositories
{
    public abstract class EntityRepository<TEntity> : RepositoryBase<TEntity>, IEntityRepository<TEntity> where TEntity : EntityBase<TEntity>
    {
        public IQueryable<TEntity> Query => dbSet;

        public Task Update(TEntity entity)
        {
            if (entity.IsTransient())
                dbSet.Add(entity);
            else if (entity.IsModified())
                dbSet.Update(entity);
            else if (entity.IsRemoved())
            {
                var readModel = entity;
                if (readModel is ISoftDelete)
                {
                    readModel.Deleted = true;
                    dbSet.Update(readModel);
                }
                else
                    dbSet.Remove(readModel);
            }

            return Task.CompletedTask;
        }
    }
}
