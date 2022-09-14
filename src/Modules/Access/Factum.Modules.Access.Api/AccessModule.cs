using Factum.Modules.Access.Core;
using Factum.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Factum.Modules.Access.Api
{
    public class AccessModule : IModule
    {
        public string Name => "Access";

        public IEnumerable<string> Policies { get; } = new[]
        {
            "access"
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
            
        }
    }
}