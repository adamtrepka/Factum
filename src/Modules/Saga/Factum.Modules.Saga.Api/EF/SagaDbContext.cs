using Factum.Modules.Saga.Api.EF.Entities;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Api.EF
{
    internal class SagaDbContext : DbContext
    {
        public static readonly string DefaultSchemaName = "saga";
        public DbSet<InboxMessage> Inbox { get; set; }
        public DbSet<OutboxMessage> Outbox { get; set; }
        public DbSet<SagaStateEntity> Sagas { get; set; }
        public DbSet<SagaLogDataEntity> Logs { get; set; }

        public SagaDbContext(DbContextOptions<SagaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
