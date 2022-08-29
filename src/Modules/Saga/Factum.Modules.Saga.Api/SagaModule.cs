using Factum.Modules.Saga.Core;
using Factum.Modules.Saga.Core.Services;
using Factum.Modules.Saga.Infrastructure;
using Factum.Shared.Abstractions.Modules;
using Factum.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Factum.Modules.Saga.Api
{
    internal class SagaModule : IModule
    {
        public string Name => "Saga";
        public IEnumerable<string> Policies { get; } = new[]
        {
            "saga"
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
            services.AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseModuleRequests()
               .Subscribe<string, string>("saga/get", (request, serviceProvider, cancellationToken) => serviceProvider.GetRequiredService<ISagaService>().GetSagaStatus(request));
        }
    }
}