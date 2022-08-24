using Factum.Modules.Ledger.Core.Domain.Entities;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.EF
{
    internal class LedgerDbContext : DbContext
    {
        public static readonly string DefaultSchemaName = "ledger";
        public DbSet<InboxMessage> Inbox { get; set; }
        public DbSet<OutboxMessage> Outbox { get; set; }
        public DbSet<Block> Blockchain { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public LedgerDbContext(DbContextOptions<LedgerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
