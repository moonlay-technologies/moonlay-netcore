using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moonlay.Domain
{
    public abstract class ReadModel : Entity
    {
        public void TransferDomainEvents(Entity entity)
        {
            entity.DomainEvents.ToList().ForEach(o => this.AddDomainEvent(o));

            entity.ClearDomainEvents();
        }
    }
}
