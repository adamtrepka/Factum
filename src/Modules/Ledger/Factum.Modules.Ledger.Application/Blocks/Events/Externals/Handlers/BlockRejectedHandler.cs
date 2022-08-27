using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Infrastructure.SqlServer;

namespace Factum.Modules.Ledger.Application.Blocks.Events.Externals.Handlers
{
    internal class BlockRejectedHandler : IEventHandler<BlockRejected>
    {
        private readonly IBlockRepository _blockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlockRejectedHandler(IBlockRepository blockRepository, IUnitOfWork unitOfWork)
        {
            _blockRepository = blockRepository ?? throw new ArgumentNullException(nameof(blockRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task HandleAsync(BlockRejected @event, CancellationToken cancellationToken = default)
        {
           /* await _unitOfWork.ExecuteAsync(async () => */await _blockRepository.DeleteAsync(@event.BlockId)/*)*/;
        }
    }
}
