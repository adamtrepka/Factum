using Factum.Modules.Saga.Api.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Reflection;

namespace Factum.Modules.Saga.Api.EF.Configurations
{
    internal class SagaLogDataConfiguration : IEntityTypeConfiguration<SagaLogDataEntity>
    {
        public void Configure(EntityTypeBuilder<SagaLogDataEntity> builder)
        {
            builder.Ignore(x => x.PrimaryKey);
            builder.HasKey(x => x.PrimaryKey);
            builder.Property(x => x.Id).HasConversion(v => v.ToString(), v => v);
            builder.HasIndex(x => new { x.Id, x.Type }).IsClustered(false);

            builder.Property(x => x.Type).HasConversion(
                v => v.FullName,
                v => Assembly.GetExecutingAssembly().GetType(v));

            builder.Property(e => e.Message).HasConversion(
            v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
            v => JsonConvert.DeserializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
