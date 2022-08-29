using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Saga.Core.Messages.External
{
    internal record BlockAdded(Guid NewBlockId, int Confirmed, int RequiredConfirmations) : IEvent;
}
