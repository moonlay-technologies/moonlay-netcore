using System.Linq;

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
