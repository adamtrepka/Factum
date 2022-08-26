using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Modules.Ledger.Core.Blocks.Types;
using Factum.Shared.Abstractions.Events;
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

        public BlockValidatedHandler(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository ?? throw new ArgumentNullException(nameof(blockRepository));
        }
        public async Task HandleAsync(BlockValidated @event, CancellationToken cancellationToken = default)
        {
            var block = await _blockRepository.GetAsync(@event.BlockId);
            block.Confirm();
            await _blockRepository.UpdateAsync(block);
        }
    }
}
