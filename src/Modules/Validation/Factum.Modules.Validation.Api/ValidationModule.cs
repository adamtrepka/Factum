using Factum.Modules.Validation.Core;
using Factum.Modules.Validation.Core.Events.Externals;
using Factum.Shared.Abstractions.Modules;
using Factum.Shared.Infrastructure.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Factum.Modules.Validation.Api
{
    public class ValidationModule : IModule
    {
        public string Name => "Validation";
        public IEnumerable<string> Policies { get; } = new[]
        {
            "validation"
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseContracts()
               .Register<UntrustedBlockAddedContract>();
        }
    }
}