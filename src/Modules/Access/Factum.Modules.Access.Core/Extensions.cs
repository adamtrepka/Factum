using Factum.Shared.Infrastructure;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Factum.Shared.Infrastructure.SqlServer;
using Factum.Modules.Access.Core.EF;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Factum.Modules.Access.Core.Repositories;
using Factum.Modules.Access.Core.EF.Repositories;
using Factum.Modules.Access.Core.Clients.Documents;

[assembly: InternalsVisibleTo("Factum.Modules.Access.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Access.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSqlServer<AccessDbContext>(defaultSchemaName: AccessDbContext.DefaultSchemaName);
            services.AddOutbox<AccessDbContext>();
            services.AddUnitOfWork<AccessUnitOfWork>();
            services.AddScoped<IAccessRepository, AccessRepository>();

            services.AddSingleton<IDocumentApiClient, DocumentApiClient>();

            return services;
        }
    }
}