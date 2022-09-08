using Factum.Modules.Ledger.Application.Blocks.DTO;
using Factum.Modules.Ledger.Application.Blocks.Events;
using Factum.Modules.Ledger.Core.Blocks.Entities;
using Factum.Modules.Ledger.Core.Blocks.Policies;
using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Modules.Ledger.Infrastructure.Clients.Saga;
using Factum.Modules.Ledger.Infrastructure.EF;
using Factum.Shared.Abstractions.Commands;
using Factum.Shared.Abstractions.Messaging;
using Factum.Shared.Infrastructure.Security.Encryption;
using Factum.Shared.Infrastructure.Security.MerkleTree;
using Factum.Shared.Infrastructure.Serialization;
using Factum.Shared.Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Factum.Modules.Ledger.Application.Blocks.Commands.Handlers
{
    internal class CreateNewBlockHandler : ICommandHandler<CreateNewBlock>
    {
        private readonly ILogger<CreateNewBlockHandler> _logger;
        private readonly IMessageBroker _messageBroker;
        private readonly IHasher _hasher;
        private readonly IBlockConfirmationPolicy _blockConfirmationPolicy;
        private readonly IBlockRepository _blockRepository;
        private readonly IEntryRepository _entryRepository;
        private readonly IMerkleTree _merkleTree;

        public CreateNewBlockHandler(ILogger<CreateNewBlockHandler> logger,
                                     IMessageBroker messageBroker,
                                     IHasher hasher,
                                     IBlockConfirmationPolicy blockConfirmationPolicy,
                                     IBlockRepository blockRepository,
                                     IEntryRepository entryRepository,
                                     IMerkleTree merkleTree)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _blockConfirmationPolicy = blockConfirmationPolicy ?? throw new ArgumentNullException(nameof(blockConfirmationPolicy));
            _blockRepository = blockRepository ?? throw new ArgumentNullException(nameof(blockRepository));
            _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
            _merkleTree = merkleTree ?? throw new ArgumentNullException(nameof(merkleTree));
        }
        public async Task HandleAsync(CreateNewBlock command, CancellationToken cancellationToken = default)
        {
            var previousBlockHash = command.PreviousBlock is not null ? _hasher.Hash(command.PreviousBlock.MapToDto()) : null;

            var numberOfEntriesToProceed = await CountNumberOfEntriesToProceed();

            var entriesToProceed = await _entryRepository.GetWithoutBlock(numberOfEntriesToProceed);

            foreach (var entry in entriesToProceed)
            {
                var entryMetadata = entry.Metadata.ToDictionary(x => x.Key, x => x.Value);
                var entryMetadataHash = _hasher.Hash(entryMetadata);
                entry.SetMetadataHash(entryMetadataHash);
            }

            var merkleTreeResult = _merkleTree.BuildTree(entriesToProceed.Select(x => x.MetadataHash));

            var newBlock = new Block(command.PreviousBlock?.BusinessId, previousBlockHash, merkleTreeResult.Root.Hash, entriesToProceed);

            await _blockRepository.AddAsync(newBlock);

            await _messageBroker.PublishAsync(new BlockAdded(newBlock.BusinessId, 0, _blockConfirmationPolicy.BlockRequiredConfirmation), cancellationToken);

            _logger.LogInformation($"Block with ID '{newBlock.BusinessId}' and '{numberOfEntriesToProceed}' entries was added.");
        }

        private async Task<int> CountNumberOfEntriesToProceed()
        {
            var x = await _entryRepository.CountWaitingToBeProcessed();
            // check for the set bits
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;

            // Then we remove all but the top bit by xor'ing the
            // string of 1's with that string of 1's shifted one to
            // the left, and we end up with just the one top bit
            // followed by 0's.
            return x ^ (x >> 1);

        }
    }
}
