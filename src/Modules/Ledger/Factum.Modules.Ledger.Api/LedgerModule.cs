using Factum.Modules.Ledger.Core;
using Factum.Modules.Ledger.Core.Events.External;
using Factum.Shared.Abstractions.Modules;
using Factum.Shared.Infrastructure.Contracts;
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
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseContracts()
               .Register<DocumentAddedContract>();
        }
    }
}