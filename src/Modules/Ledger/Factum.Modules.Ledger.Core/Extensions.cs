using Factum.Modules.Ledger.Core.Blocks.Policies;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Api")]
[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Application")]
[assembly: InternalsVisibleTo("Factum.Modules.Ledger.Infrastructure")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Ledger.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IBlockCreationPolicy, BlockCreationPolicy>();
            services.AddScoped<IBlockConfirmationPolicy, BlockConfirmationPolicy>();

            return services;
        }
    }
}