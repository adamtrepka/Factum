using Chronicle;
using Factum.Modules.Saga.Api.Sagas;
using Factum.Modules.Saga.Api.Services;
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
            services.AddChronicle();
            services.AddSingleton<ISagaService, SagaService>();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseModuleRequests()
               .Subscribe<string, string>("saga/get", (request, serviceProvider, cancellationToken) => serviceProvider.GetRequiredService<ISagaService>().GetSagaStatus(request));
        }
    }
}