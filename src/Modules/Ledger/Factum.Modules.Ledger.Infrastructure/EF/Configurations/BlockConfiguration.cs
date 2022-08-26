using Factum.Modules.Ledger.Core.Blocks.Entities;
using Factum.Modules.Ledger.Core.Blocks.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Infrastructure.EF.Configurations
{
    internal class BlockConfiguration : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.BusinessId).IsUnique();
            builder.Property(x => x.Version).IsConcurrencyToken();
            builder.Ignore(x => x.Events);

            builder.Property(x => x.BusinessId)
                   .HasConversion(x => x.Value, x => new BlockId(x));

            builder.Property(x => x.PreviousBlockId)
                   .HasConversion(x => x.Value, x => new BlockId(x));

            builder.HasMany(x => x.Entries).WithOne(x => x.Block).HasForeignKey(x => x.BlockId);

            builder.HasOne(x => x.PreviousBlock).WithOne().HasForeignKey<Block>(x => x.PreviousBlockId).HasPrincipalKey<Block>(x => x.BusinessId);
        }
    }
}
