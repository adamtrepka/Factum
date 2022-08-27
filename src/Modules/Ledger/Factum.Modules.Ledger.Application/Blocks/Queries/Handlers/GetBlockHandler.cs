using Factum.Modules.Ledger.Application.Blocks.DTO;
using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Blocks.Queries.Handlers
{
    internal class GetBlockHandler : IQueryHandler<GetBlock, BlockDetailsDto>
    {
        private readonly IBlockRepository _blockRepository;

        public GetBlockHandler(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository ?? throw new ArgumentNullException(nameof(blockRepository));
        }
        public async Task<BlockDetailsDto> HandleAsync(GetBlock query, CancellationToken cancellationToken = default)
        {
            var block = await _blockRepository.GetAsync(query.Id);
            return block?.MapToDto();
        }
    }
}
