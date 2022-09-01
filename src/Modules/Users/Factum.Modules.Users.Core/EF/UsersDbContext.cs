using Microsoft.EntityFrameworkCore;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Factum.Modules.Users.Core.Entities;

namespace Factum.Modules.Users.Core.EF;

internal class UsersDbContext : DbContext
{
    public static readonly string DefaultSchemaName = "users";
    public DbSet<InboxMessage> Inbox { get; set; }
    public DbSet<OutboxMessage> Outbox { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}