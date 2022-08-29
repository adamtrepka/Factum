using Chronicle;
using Factum.Modules.Saga.Infrastructure.EF.Chronicle;
using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Converters;
using Factum.Shared.Infrastructure.SqlServer;
using Factum.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Repositories;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Factum.Modules.Saga.Infrastructure.EF.Saga;

[assembly: InternalsVisibleTo("Factum.Modules.Saga.Api")]
[assembly: InternalsVisibleTo("Factum.Modules.Saga.Core")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Factum.Modules.Saga.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IChronicleTypeConverter, ChronicleTypeConverter>();

            services.AddSqlServer<SagaDbContext>(defaultSchemaName: SagaDbContext.DefaultSchemaName);
            services.AddOutbox<SagaDbContext>();

            services.AddChronicle(config =>
            {
                config.UseDatabasePersistence();
            });

            return services;
        }

        private static IServiceCollection AddChronicleDatabase(this IServiceCollection services)
        {
            var options = services.GetOptions<SqlServerOptions>("sqlserver");

            services.AddDbContext<ChronicleDbContext>(x => x.UseSqlServer(options.ConnectionString, options =>
            {
                options.MigrationsHistoryTable("__MigrationsHistory", ChronicleDbContext.DefaultSchemaName);
            }),
            optionsLifetime: ServiceLifetime.Transient,
            contextLifetime: ServiceLifetime.Transient);

            return services;
        }

        private static IChronicleBuilder UseDatabasePersistence(this IChronicleBuilder builder)
        {
            builder.Services.AddChronicleDatabase();

            builder.UseSagaStateRepository<SagaStateRepository>();
            builder.UseSagaLog<SagaLog>();
            return builder;
        }
    }
}