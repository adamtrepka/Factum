using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Modules.Documents.Infrastructure.EF;
using Factum.Modules.Documents.Infrastructure.EF.Repositories;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Factum.Shared.Infrastructure.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("Factum.Modules.Documents.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Documents.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddScoped<IDocumentRepository, DocumentRepository>()
                       .AddScoped<IAccessRepository,AccessRepository>()
                       .AddSqlServer<DocumentsDbContext>(defaultSchemaName: DocumentsDbContext.DefaultSchemaName)
                       .AddOutbox<DocumentsDbContext>()
                       .AddUnitOfWork<DocumentsUnitOfWork>();
    }
}

