using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Saga.Core.Messages.External
{
    internal record BlockRejected(Guid BlockId) : IEvent;

}
