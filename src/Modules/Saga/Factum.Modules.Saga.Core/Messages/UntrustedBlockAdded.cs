using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Saga.Core.Messages
{
    internal record UntrustedBlockAdded(Guid BlockId) : IEvent;
}
