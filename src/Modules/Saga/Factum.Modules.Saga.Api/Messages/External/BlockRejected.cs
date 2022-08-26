using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Saga.Api.Messages.External
{
    internal record BlockRejected(Guid BlockId) : IEvent;

}
