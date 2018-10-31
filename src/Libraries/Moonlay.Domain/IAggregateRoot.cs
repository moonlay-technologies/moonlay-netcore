using System;

namespace Moonlay.Domain
{

    public interface IAggregateRoot
    {
        Guid Identity { get; }
    }

}
