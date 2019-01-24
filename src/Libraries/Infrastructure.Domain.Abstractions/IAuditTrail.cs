using System;

namespace Core.Domain
{
    public interface IAuditTrail
    {
        string CreatedBy { get; }

        DateTimeOffset CreatedDate { get; }

        string ModifiedBy { get; }

        DateTimeOffset? ModifiedDate { get; }
    }

    public interface ISoftDelete
    {
        bool? Deleted { get; }

        DateTimeOffset? DeletedDate { get; }

        string DeletedBy { get; }
    }
}