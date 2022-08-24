using Factum.Modules.Ledger.Core.Clients.Documents;
using Factum.Modules.Ledger.Core.EF;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Factum.Shared.Infrastructure.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Ledger.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentApiClient, DocumentApiClient>();
            services.AddSqlServer<LedgerDbContext>(defaultSchemaName: LedgerDbContext.DefaultSchemaName);
            services.AddOutbox<LedgerDbContext>();
            services.AddUnitOfWork<LedgerUnitOfWork>();

            return services;
        }
    }
}