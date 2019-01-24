using System.Linq;

namespace Core.Domain
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