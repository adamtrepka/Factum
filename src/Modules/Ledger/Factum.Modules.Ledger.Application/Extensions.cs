using Factum.Modules.Ledger.Application.Entries.Clients.Documents;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Api")]
[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Infrastructure")]
[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Core")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Ledger.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IDocumentApiClient, DocumentApiClient>();

        return services;
    }
}
