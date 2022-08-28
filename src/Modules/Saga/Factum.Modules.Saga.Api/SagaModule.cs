using Chronicle;
using Factum.Modules.Saga.Api.EF;
using Factum.Modules.Saga.Api.EF.Repositories;
using Factum.Modules.Saga.Api.Sagas;
using Factum.Modules.Saga.Api.Services;
using Factum.Shared.Abstractions.Modules;
using Factum.Shared.Infrastructure;
using Factum.Shared.Infrastructure.Messaging.Outbox;
using Factum.Shared.Infrastructure.Modules;
using Factum.Shared.Infrastructure.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
            var options = services.GetOptions<SqlServerOptions>("sqlserver");
            services.AddDbContext<SagaDbContext>(x => x.UseSqlServer(options.ConnectionString, options =>
            {
                options.MigrationsHistoryTable("__MigrationsHistory", SagaDbContext.DefaultSchemaName);
            }),optionsLifetime: ServiceLifetime.Transient);

            //services.AddOutbox<SagaDbContext>();
            //services.AddScoped<ISagaStateRepository, SagaStateRepository>();
            //services.AddScoped<ISagaLog, SagaLogger>();

            services.AddChronicle(config =>
            {
                config.UseSagaStateRepository<SagaStateRepository>();
                config.UseSagaLog<SagaLogger>();
            });
            services.AddTransient<ISagaService, SagaService>();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseModuleRequests()
               .Subscribe<string, string>("saga/get", (request, serviceProvider, cancellationToken) => serviceProvider.GetRequiredService<ISagaService>().GetSagaStatus(request));
        }
    }
}