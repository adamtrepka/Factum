using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Access.Core.EF.Configurations
{
    internal class AccessEntityTypeConfiguration : IEntityTypeConfiguration<Entities.Access>
    {
        public void Configure(EntityTypeBuilder<Entities.Access> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.BusinessId).IsClustered(false).IsUnique();
            builder.HasIndex(x => new { x.DocumentId, x.GrantedTo });
            builder.HasIndex(x => new { x.DocumentId, x.GrantedBy });

            builder.Property(x => x.DocumentId)
                   .HasConversion(x => x.Value, x => new(x));

            builder.Property(x => x.GrantedBy)
                   .HasConversion(x => x.Value, x => new(x));

            builder.Property(x => x.GrantedTo)
                   .HasConversion(x => x.Value, x => new(x));

            builder.Property(x => x.Type)
                   .HasConversion(x => x.Value, x => new(x));
        }
    }
}
