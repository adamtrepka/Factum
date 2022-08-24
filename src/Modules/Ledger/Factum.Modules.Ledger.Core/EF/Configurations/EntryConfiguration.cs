using Factum.Modules.Ledger.Core.Domain.Entities;
using Factum.Modules.Ledger.Core.Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factum.Modules.Ledger.Core.EF.Configurations
{
    internal class EntryConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.BusinessId).IsUnique();

            builder.Property(x => x.BusinessId)
                   .HasConversion(x => x.Value, x => new EntryId(x));

            builder.Property(x => x.BlockId)
                   .HasConversion(x => x.Value, x => new BlockId(x));

            builder.HasOne(x => x.Block).WithMany(x => x.Entries).HasForeignKey(x => x.BlockId).HasPrincipalKey(x => x.BusinessId).IsRequired(false);
        }
    }
}
