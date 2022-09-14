
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Factum.Modules.Access.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Access.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services;
        }
    }
}