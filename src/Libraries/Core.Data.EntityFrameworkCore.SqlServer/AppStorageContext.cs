﻿using Core.Domain;
using Core.Domain.ReadModels;
using Core.Mvc;
using ExtCore.Data.EntityFramework;
using ExtCore.Data.EntityFramework.SqlServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Data.EntityFrameworkCore
{
    public class AppStorageContext : StorageContext
    {
        private readonly IWorkContext _workContext;
        private readonly IMediator _mediator;

        public AppStorageContext(IOptions<StorageContextOptions> options, IWorkContext workContext, IMediator mediator) : base(options)
        {
            _workContext = workContext;
            _mediator = mediator;
        }

        public override int SaveChanges()
        {
            this.AuditTrack(_workContext);

            this.DispatchDomainEventsAsync(_mediator).Wait();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.AuditTrack(_workContext);

            await this.DispatchDomainEventsAsync(_mediator);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }

    internal static class EventDispatcherExtension
    {
        public static void AuditTrack(this AppStorageContext ctx, IWorkContext workContext)
        {
            if (workContext == null) return;

            var now = DateTime.Now;

            var addedAuditedEntities = ctx.ChangeTracker.Entries<ReadModelBase>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var modifiedAuditedEntities = ctx.ChangeTracker.Entries<ReadModelBase>()
              .Where(p => p.State == EntityState.Modified)
              .Select(p => p.Entity);

            if (!modifiedAuditedEntities.Any() && !addedAuditedEntities.Any())
                return;

            var currentUser = workContext.CurrentUser ?? "System";

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedBy = currentUser;
                added.CreatedDate = now;
                added.Deleted = false;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.ModifiedBy = currentUser;
                modified.ModifiedDate = now;

                if (modified is ISoftDelete)
                {
                    if (modified.Deleted.HasValue && modified.Deleted.Value)
                    {
                        modified.DeletedBy = currentUser;
                        modified.DeletedDate = now;
                    }
                }
            }
        }

        public static async Task DispatchDomainEventsAsync(this AppStorageContext ctx, IMediator mediator)
        {
            if (mediator == null) return;

            var domainEntities = ctx.ChangeTracker
                .Entries<ReadModelBase>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async d => await mediator.Publish(d));

            await Task.WhenAll(tasks);
        }
    }

}