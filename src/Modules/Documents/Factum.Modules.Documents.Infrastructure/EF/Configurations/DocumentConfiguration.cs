using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.OwnsOne(x => x.File);
        }
    }
}
