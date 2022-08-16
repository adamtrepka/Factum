using Factum.Shared.Abstractions.Commands;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Queries;
using Factum.Shared.Infrastructure.SqlServer.Decorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.SqlServer;

public static class Extensions
{
    public static Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, IPagedQuery query,
        CancellationToken cancellationToken = default)
        => data.PaginateAsync(query.Page, query.Results, cancellationToken);

    public static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, int page, int results,
        CancellationToken cancellationToken = default)
    {
        if (page <= 0)
        {
            page = 1;
        }

        results = results switch
        {
            <= 0 => 10,
            > 100 => 100,
            _ => results
        };

        var totalResults = await data.CountAsync();
        var totalPages = totalResults <= results ? 1 : (int)Math.Floor((double)totalResults / results);
        var result = await data.Skip((page - 1) * results).Take(results).ToListAsync(cancellationToken);

        return new Paged<T>(result, page, results, totalPages, totalResults);
    }

    public static Task<List<T>> SkipAndTakeAsync<T>(this IQueryable<T> data, IPagedQuery query,
        CancellationToken cancellationToken = default)
        => data.SkipAndTakeAsync(query.Page, query.Results, cancellationToken);

    public static async Task<List<T>> SkipAndTakeAsync<T>(this IQueryable<T> data, int page, int results,
        CancellationToken cancellationToken = default)
    {
        if (page <= 0)
        {
            page = 1;
        }

        results = results switch
        {
            <= 0 => 10,
            > 100 => 100,
            _ => results
        };

        return await data.Skip((page - 1) * results).Take(results).ToListAsync(cancellationToken);
    }

    public static IServiceCollection AddSqlServer(this IServiceCollection services)
    {
        var options = services.GetOptions<SqlServerOptions>("sqlserver");
        services.AddSingleton(options);
        services.AddSingleton(new UnitOfWorkTypeRegistry());

        return services;
    }

    public static IServiceCollection AddTransactionalDecorators(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(TransactionalCommandHandlerDecorator<>));
        services.TryDecorate(typeof(IEventHandler<>), typeof(TransactionalEventHandlerDecorator<>));

        return services;
    }

    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<SqlServerOptions>("sqlserver");
        services.AddDbContext<T>(x => x.UseSqlServer(options.ConnectionString));

        return services;
    }

    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : class, IUnitOfWork
    {
        services.AddScoped<IUnitOfWork, T>();
        services.AddScoped<T>();
        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<T>();

        return services;
    }
}