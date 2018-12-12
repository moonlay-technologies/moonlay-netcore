using System;

namespace Moonlay.Domain
{
    public interface IAuditTrail
    {
        string CreatedBy { get; set; }

        DateTimeOffset CreatedDate { get; set; }

        string ModifiedBy { get; set; }

        DateTimeOffset? ModifiedDate { get; set; }
    }

    public interface ISoftDelete
    {
        bool? Deleted { get; set; }

        DateTimeOffset? DeletedDate { get; set; }

        string DeletedBy { get; set; }
    }
}
