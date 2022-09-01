using Factum.Shared.Abstractions.Messaging;
using System.Collections.Generic;

namespace Factum.Shared.Abstractions.Kernel;

public interface IEventMapper
{
    IMessage Map(IDomainEvent @event);
    IMessage[] MapAll(IEnumerable<IDomainEvent> events);
}