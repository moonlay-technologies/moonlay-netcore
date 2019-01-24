using MediatR;

namespace Core.Domain.Events
{
    public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
    {
    }
}