using Chronicle;
using Factum.Shared.Abstractions.Modules;
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
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}