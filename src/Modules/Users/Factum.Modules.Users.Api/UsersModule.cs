using Factum.Modules.Users.Core;
using Factum.Modules.Users.Core.DTO;
using Factum.Modules.Users.Core.Queries;
using Factum.Shared.Abstractions.Modules;
using Factum.Shared.Abstractions.Queries;
using Factum.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Factum.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public string Name { get; } = "Users";

        public IEnumerable<string> Policies { get; } = new[]
        {
            "users"
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseModuleRequests()
                .Subscribe<GetUserByEmail, UserDetailsDto>("users/get",
                    (query, serviceProvider, cancellationToken) =>
                        serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
        }
    }
}