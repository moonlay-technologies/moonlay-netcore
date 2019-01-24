using System;

namespace Core.Domain
{
    public interface IAggregateRoot
    {
        Guid Identity { get; }
    }
}