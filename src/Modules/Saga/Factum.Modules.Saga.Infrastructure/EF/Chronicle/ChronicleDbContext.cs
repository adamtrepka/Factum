using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle
{
    internal class ChronicleDbContext : DbContext
    {
        public static readonly string DefaultSchemaName = "chronicle";

        public DbSet<SagaStateEntity> Sagas { get; set; }
        public DbSet<SagaLogEntity> Logs { get; set; }
        public ChronicleDbContext(DbContextOptions<ChronicleDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
