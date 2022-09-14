using Factum.Modules.Ledger.Application;
using Factum.Modules.Ledger.Application.Blocks.DTO;
using Factum.Modules.Ledger.Application.Blocks.Events.Externals;
using Factum.Modules.Ledger.Application.Blocks.Queries;
using Factum.Modules.Ledger.Application.Entries.Events.External;
using Factum.Modules.Ledger.Core;
using Factum.Modules.Ledger.Infrastructure;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Abstractions.Modules;
using Factum.Shared.Infrastructure.Contracts;
using Factum.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Factum.Modules.Ledger.Api
{
    public class LedgerModule : IModule
    {
        public const string LedgerPolicyName = "ledger";

        public string Name => "Ledger";

        public IEnumerable<string> Policies { get; } = new[]
        {
            LedgerPolicyName
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
            services.AddApplication();
            services.AddInfrastructure();   
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseContracts()
               .Register<DocumentAddedContract>()
               .Register<BlockValidatedContract>()
               .Register<BlockRejectedContract>()
               .Register<SagaComplatedContract>()
               .Register<SagaRejectedContract>()
               .Register<AccessGrantedContract>()
               .Register<AccessRevokedContract>();

            app.UseModuleRequests()
               .Subscribe<GetBlock, BlockDetailsDto>("ledger/get",
                    (query, serviceProvider, cancellationToken) => serviceProvider.GetRequiredService<IDispatcher>()
                                                                                  .QueryAsync(query, cancellationToken));
        }
    }
}