using Factum.Modules.Ledger.Core.Blocks.Entities;
using Factum.Modules.Ledger.Core.Blocks.Policies;
using Factum.Modules.Ledger.Infrastructure.Clients.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Blocks.Policies
{
    internal class BlockSagaStatusPolicy : IBlockSagaStatusPolicy
    {
        private readonly ISagaApiClient _sagaApiClient;

        public BlockSagaStatusPolicy(ISagaApiClient sagaApiClient)
        {
            _sagaApiClient = sagaApiClient ?? throw new ArgumentNullException(nameof(sagaApiClient));
        }
        public async Task<bool> IsPending(Block block)
        {
            if (block is null) return false;
            var sagaStatus = await _sagaApiClient.Get(block.BusinessId.ToString());
            return sagaStatus == "pending";
        }
    }
}
