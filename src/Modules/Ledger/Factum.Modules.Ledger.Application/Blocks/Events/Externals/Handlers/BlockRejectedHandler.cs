using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Ledger.Application.Blocks.Events.Externals.Handlers
{
    internal class BlockRejectedHandler : IEventHandler<BlockRejected>
    {
        private readonly IBlockRepository _blockRepository;

        public BlockRejectedHandler(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository ?? throw new ArgumentNullException(nameof(blockRepository));
        }
        public async Task HandleAsync(BlockRejected @event, CancellationToken cancellationToken = default)
        {
            await _blockRepository.DeleteAsync(@event.BlockId);
        }
    }
}
