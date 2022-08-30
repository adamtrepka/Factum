using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Modules.Ledger.Core.Entries.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factum.Modules.Ledger.Infrastructure.EF.Configurations
{
    internal class EntryMetadataConfiguration : IEntityTypeConfiguration<EntryMetadata>
    {
        public void Configure(EntityTypeBuilder<EntryMetadata> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.Key, x.Value }).IsClustered(false);

            builder.Property(x => x.EntryId)
                   .HasConversion(x => x.Value, x => new EntryId(x));

            builder.HasOne(x => x.Entry).WithMany(x => x.Metadata).HasPrincipalKey(x => x.BusinessId).HasForeignKey(x => x.EntryId).IsRequired();
        }
    }
}
