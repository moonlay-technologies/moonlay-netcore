using ExtCore.Data.EntityFramework;
using Core.Domain;
using Core.Domain.ReadModels;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Data.EntityFrameworkCore
{
    public abstract class AggregateRepostory<TAggregate, TReadModel> : RepositoryBase<TReadModel>, IAggregateRepository<TAggregate, TReadModel>
        where TAggregate : AggregateRoot<TAggregate, TReadModel>
        where TReadModel : ReadModelBase
    {
        public IQueryable<TReadModel> Query => dbSet;

        public List<TAggregate> Find(IQueryable<TReadModel> readModels)
        {
            return readModels.Select(o => Map(o)).ToList();
        }

        public List<TAggregate> Find(Expression<Func<TReadModel, bool>> condition)
        {
            return Query.Where(condition).Select(o => Map(o)).ToList();
        }

        protected abstract TAggregate Map(TReadModel readModel);

        public virtual Task Update(TAggregate aggregate)
        {
            if (aggregate.IsTransient())
                dbSet.Add(aggregate.GetReadModel());
            else if (aggregate.IsModified())
                dbSet.Update(aggregate.GetReadModel());
            else if (aggregate.IsRemoved())
            {
                var readModel = aggregate.GetReadModel();
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