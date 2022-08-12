using Factum.Shared.Abstractions.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.Messaging.Dispatchers;

public interface IAsyncMessageDispatcher
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : class, IMessage;
}