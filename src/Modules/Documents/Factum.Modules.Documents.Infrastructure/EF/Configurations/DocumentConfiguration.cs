using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factum.Modules.Documents.Infrastructure.EF.Configurations
{
    internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.BusinessId).IsUnique();
            builder.Property(x => x.Version).IsConcurrencyToken();
            builder.Ignore(x => x.Events);

            builder.Property(x => x.BusinessId)
                .HasConversion(x => x.Value, x => new DocumentId(x));

            builder.OwnsOne(x => x.File)
                .Property(x => x.Hash).IsRequired(true);

            builder.Navigation(x => x.Entitlements).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany(x => x.Entitlements,navigationBuilder =>
            {
                navigationBuilder.HasKey(c => c.Id);
                navigationBuilder.Property(c => c.UserId)
                                 .HasConversion(x => x.Value, x => new(x));

                navigationBuilder.Property(x => x.DocumentId)
                                 .HasConversion(x => x.Value, x => new(x));

                navigationBuilder.WithOwner().HasForeignKey(x => x.DocumentId).HasPrincipalKey(x => x.BusinessId);
            });
        }
    }
}
