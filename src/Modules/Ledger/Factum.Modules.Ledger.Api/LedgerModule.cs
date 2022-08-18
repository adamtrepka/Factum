using Factum.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Factum.Modules.Ledger.Api
{
    public class LedgerModule : IModule
    {
        public static readonly string LedgerPolicyName = "ledger";

        public string Name => "Ledger";

        public IEnumerable<string> Policies { get; } = new[]
        {
            LedgerPolicyName
        };

        public void Register(IServiceCollection services)
        {
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}