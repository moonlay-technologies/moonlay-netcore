using Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.EntityFrameworkCore
{
    public static class EntityTypeBuilderExtension
    {
        public static void ApplyAuditTrail<T>(this EntityTypeBuilder<T> builder) where T : class, IAuditTrail
        {
            builder.Property(p => p.CreatedBy).HasMaxLength(32).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();

            builder.Property(p => p.ModifiedBy).HasMaxLength(32);
        }

        public static void ApplySoftDelete<T>(this EntityTypeBuilder<T> builder) where T : class, ISoftDelete
        {
            builder.Property(p => p.DeletedBy).HasMaxLength(32);

            builder.HasQueryFilter(p => p.Deleted == null || (p.Deleted.HasValue && !p.Deleted.Value));
        }
    }
}