using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Factum.Shared.Infrastructure;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Factum.Shared.Infrastructure.SqlServer;
using Factum.Modules.Users.Core.Services;
using Factum.Modules.Users.Core.Repositories;
using Factum.Modules.Users.Core.Entities;
using Factum.Modules.Users.Core.EF.Repositories;
using Factum.Modules.Users.Core.EF;

[assembly: InternalsVisibleTo("Factum.Modules.Users.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]


namespace Factum.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var registrationOptions = services.GetOptions<RegistrationOptions>("users:registration");
        services.AddSingleton(registrationOptions);

        services.AddSqlServer<UsersDbContext>(defaultSchemaName: UsersDbContext.DefaultSchemaName);
        services.AddOutbox<UsersDbContext>();
        services.AddUnitOfWork<UsersUnitOfWork>();

        services.AddSingleton<IUserRequestStorage, UserRequestStorage>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddInitializer<UsersInitializer>();

        return services;
    }
}