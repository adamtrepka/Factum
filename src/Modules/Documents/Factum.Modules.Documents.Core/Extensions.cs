using Factum.Modules.Documents.Application.Documents.Policies;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Factum.Modules.Documents.Api")]
[assembly: InternalsVisibleTo("Factum.Modules.Documents.Application")]
[assembly: InternalsVisibleTo("Factum.Modules.Documents.Infrastructure")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Documents.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IDocumentAccessPolicy, DocumentAccessPolicy>();
        services.AddScoped<IDocumentGrantAccessPolicy, DocumentGrantAccessPolicy>();
        services.AddScoped<IDocumentRevokeAccessPolicy, DocumentRevokeAccessPolicy>();
        
        return services;
    }
}

