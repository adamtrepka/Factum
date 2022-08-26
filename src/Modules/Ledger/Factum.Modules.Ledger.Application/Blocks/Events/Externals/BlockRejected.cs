using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;

namespace Factum.Modules.Ledger.Application.Blocks.Events.Externals
{
    internal record BlockRejected(Guid BlockId) : IEvent;

    [Message("Validation")]
    internal class BlockRejectedContract : Contract<BlockRejected>
    {
        public BlockRejectedContract()
        {
            RequireAll();
        }
    }
}
