using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Modules.Ledger.Core.Blocks.Types;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Infrastructure.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Blocks.Events.Externals.Handlers
{
    internal class BlockValidatedHandler : IEventHandler<BlockValidated>
    {
        private readonly IBlockRepository _blockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlockValidatedHandler(IBlockRepository blockRepository, IUnitOfWork unitOfWork)
        {
            _blockRepository = blockRepository ?? throw new ArgumentNullException(nameof(blockRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task HandleAsync(BlockValidated @event, CancellationToken cancellationToken = default)
        {
            //await _unitOfWork.ExecuteAsync(async () =>
            //{
                var block = await _blockRepository.GetAsync(@event.BlockId);
                block.Confirm();
                await _blockRepository.UpdateAsync(block);
            //});

        }
    }
}
