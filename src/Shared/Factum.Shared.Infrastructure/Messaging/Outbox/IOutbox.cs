using Factum.Shared.Abstractions.Messaging;
using System;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.Messaging.Outbox;

public interface IOutbox
{
    bool Enabled { get; }
    Task SaveAsync(params IMessage[] messages);
    Task PublishUnsentAsync();
    Task CleanupAsync(DateTime? to = null);
}