using Chronicle;
using Factum.Modules.Saga.Api.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Api.EF.Configurations
{
    internal class SagaStateConfiguration : IEntityTypeConfiguration<SagaStateEntity>
    {
        public void Configure(EntityTypeBuilder<SagaStateEntity> builder)
        {
            builder.HasKey(x => x.PrimaryKey);
            builder.Property(x => x.Id).HasConversion(v => v.ToString(), v => v);
            builder.HasIndex(x => new {x.Id, x.TypeName}).IsClustered(false);

            builder.Ignore(x => x.Type);
            builder.Property(x => x.TypeName).HasColumnName("Type");

            builder.Ignore(x => x.Data);
            builder.Property(x => x.DataJson).HasColumnName("Data");
        }
    }
}
