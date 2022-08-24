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
            return services;
        }
    }
}