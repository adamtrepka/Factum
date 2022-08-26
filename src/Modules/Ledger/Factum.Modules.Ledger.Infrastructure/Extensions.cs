using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Modules.Ledger.Infrastructure.EF;
using Factum.Modules.Ledger.Infrastructure.EF.Repositories;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Factum.Shared.Infrastructure.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Api")]
[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Core")]
[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Application")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Ledger.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSqlServer<LedgerDbContext>(defaultSchemaName: LedgerDbContext.DefaultSchemaName);
        services.AddOutbox<LedgerDbContext>();
        services.AddUnitOfWork<LedgerUnitOfWork>();
        services.AddScoped<IBlockRepository, BlockRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();

        return services;
    }
}