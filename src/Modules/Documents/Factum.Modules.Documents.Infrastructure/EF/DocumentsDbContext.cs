using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Factum.Modules.Documents.Infrastructure.EF;

internal class DocumentsDbContext : DbContext
{
    public DbSet<InboxMessage> Inbox { get; set; }
    public DbSet<OutboxMessage> Outbox { get; set; }
    public DbSet<Document> Documents { get; set; }

    public DocumentsDbContext(DbContextOptions<DocumentsDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("documents");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}

