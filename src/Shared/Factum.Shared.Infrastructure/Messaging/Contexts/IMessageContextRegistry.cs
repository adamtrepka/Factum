using Factum.Shared.Abstractions.Messaging;

namespace Factum.Shared.Infrastructure.Messaging.Contexts;

public interface IMessageContextRegistry
{
    void Set(IMessage message, IMessageContext context);
}