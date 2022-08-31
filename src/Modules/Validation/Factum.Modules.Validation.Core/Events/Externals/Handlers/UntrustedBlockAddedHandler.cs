using Factum.Modules.Validation.Core.Clients.Ledger;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;
using Factum.Shared.Infrastructure.Security.Encryption;
using Factum.Shared.Infrastructure.Security.MerkleTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Validation.Core.Events.Externals.Handlers
{
    internal class UntrustedBlockAddedHandler : IEventHandler<UntrustedBlockAdded>
    {
        private readonly IHasher _hasher;
        private readonly ILedgerApiClient _ledgerApiClient;
        private readonly IMessageBroker _messageBroker;
        private readonly IMerkleTree _merkleTree;

        public UntrustedBlockAddedHandler(IHasher hasher, ILedgerApiClient ledgerApiClient, IMessageBroker messageBroker, IMerkleTree merkleTree)
        {
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _ledgerApiClient = ledgerApiClient ?? throw new ArgumentNullException(nameof(ledgerApiClient));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _merkleTree = merkleTree ?? throw new ArgumentNullException(nameof(merkleTree));
        }
        public async Task HandleAsync(UntrustedBlockAdded @event, CancellationToken cancellationToken = default)
        {
            var block = await _ledgerApiClient.GetAsync(@event.BlockId, cancellationToken);

            if(block is null)
            {
                await _messageBroker.PublishAsync(new BlockRejected(@event.BlockId), cancellationToken);
                return;
            }

            var previousBlockTestResult = true;
            var metadataHashTestResult = true;
            var merkleTreeTestResult = true;

            if (block.PreviousBlockId.HasValue)
            {
                var previousBlock = await _ledgerApiClient.GetAsync(block.PreviousBlockId.Value, cancellationToken);
                previousBlockTestResult = _hasher.Validate(previousBlock, block.PreviousBlockHash);
            }

            metadataHashTestResult = block.Entries.All(x => _hasher.Validate(x.Metadata, x.MetadataHash));

            var merkleTreeResult = _merkleTree.BuildTree(block.Entries.Select(x => x.MetadataHash));

            merkleTreeTestResult = merkleTreeResult.Root.Hash.SequenceEqual(block.EntriesRootHash);

            if (previousBlockTestResult && metadataHashTestResult && merkleTreeTestResult is true)
            {
                await _messageBroker.PublishAsync(new BlockValidated(@event.BlockId), cancellationToken);
            }
            else
            {
                await _messageBroker.PublishAsync(new BlockRejected(@event.BlockId), cancellationToken);
            }
        }
    }
}
