using Factum.Shared.Abstractions.Commands;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Shared.Abstractions.Dispatchers;

public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}