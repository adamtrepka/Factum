using Factum.Shared.Abstractions.Messaging;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.Messaging.Outbox;

public interface IOutboxBroker
{
    bool Enabled { get; }
    Task SendAsync(params IMessage[] messages);
}