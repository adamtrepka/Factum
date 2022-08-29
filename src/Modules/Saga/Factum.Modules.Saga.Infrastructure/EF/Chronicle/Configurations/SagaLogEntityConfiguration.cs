using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Configurations
{
    internal class SagaLogEntityConfiguration : IEntityTypeConfiguration<SagaLogEntity>
    {
        public void Configure(EntityTypeBuilder<SagaLogEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.Type, x.SagaId });
        }
    }
}
