using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Ledger.Application.Blocks.Events
{
    internal record BlockAdded(Guid NewBlockId, int Confirmed, int RequiredConfirmations) : IEvent;
}
