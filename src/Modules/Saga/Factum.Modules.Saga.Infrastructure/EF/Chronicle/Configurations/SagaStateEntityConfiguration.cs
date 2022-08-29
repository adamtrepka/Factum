using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Configurations
{
    internal class SagaStateEntityConfiguration : IEntityTypeConfiguration<SagaStateEntity>
    {
        public void Configure(EntityTypeBuilder<SagaStateEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.Type, x.SagaId });
        }
    }
}
