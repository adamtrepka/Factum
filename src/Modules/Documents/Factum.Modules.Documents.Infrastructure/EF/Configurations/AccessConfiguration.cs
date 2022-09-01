using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factum.Modules.Documents.Infrastructure.EF.Configurations
{
    internal class AccessConfiguration : IEntityTypeConfiguration<Access>
    {
        public void Configure(EntityTypeBuilder<Access> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.BusinessId).IsUnique();

            builder.HasIndex(x => new { x.DocumentId, x.AccessType, x.GrantedTo }).IsClustered(false);

            builder.Property(x => x.BusinessId)
                   .HasConversion(x => x.Value, x => new AccessId(x));

            builder.Property(x => x.DocumentId)
                   .HasConversion(x => x.Value, x => new DocumentId(x));

            builder.Property(x => x.AccessType)
                   .HasConversion(x => x.Value, x => new AccessType(x));

            builder.Property(x => x.GrantedBy)
                   .HasConversion(x => x.Value, x => new UserId(x));

            builder.Property(x => x.GrantedTo)
                   .HasConversion(x => x.Value, x => new UserId(x));

            builder.HasOne(x => x.Document).WithMany(x => x.Accesses).HasPrincipalKey(x => x.BusinessId).HasForeignKey(x => x.DocumentId);
        }
    }
}
