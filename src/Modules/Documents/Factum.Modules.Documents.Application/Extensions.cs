using Factum.Modules.Documents.Application.Documents.Events;
using Factum.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Factum.Modules.Documents.Api")]
[assembly: InternalsVisibleTo("Factum.Modules.Documents.Infrastructure")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Documents.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, EventMapper>();
        return services;
    }
}
