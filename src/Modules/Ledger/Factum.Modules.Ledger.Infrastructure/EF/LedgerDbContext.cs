using Factum.Modules.Ledger.Core.Blocks.Entities;
using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Factum.Modules.Ledger.Infrastructure.EF
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
