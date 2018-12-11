using ExtCore.Data.Abstractions;
using ExtCore.Data.EntityFramework;
using Moonlay.Domain;

namespace Moonlay.ExtCore.EntityFrameworkCore
{
    public class RepositoryMoonlay<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> where TEntity : ReadModelExtCore
    {
        public IUnitOfWork UnitOfWork { get; private set; }

        public new void SetStorageContext(IStorageContext storageContext)
        {
            UnitOfWork = storageContext as IUnitOfWork;

            base.SetStorageContext(storageContext);
        }
    }
}
