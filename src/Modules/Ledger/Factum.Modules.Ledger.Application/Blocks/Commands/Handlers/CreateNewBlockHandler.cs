using Factum.Modules.Ledger.Application.Blocks.DTO;
using Factum.Modules.Ledger.Application.Blocks.Events;
using Factum.Modules.Ledger.Core.Blocks.Entities;
using Factum.Modules.Ledger.Core.Blocks.Policies;
using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Modules.Ledger.Infrastructure.EF;
using Factum.Shared.Abstractions.Commands;
using Factum.Shared.Abstractions.Messaging;
using Factum.Shared.Infrastructure.Security.Encryption;
using Factum.Shared.Infrastructure.Serialization;
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
        private readonly IBlockCreationPolicy _blockCreationPolicy;
        private readonly IBlockRepository _blockRepository;
        private readonly IEntryRepository _entryRepository;

        public CreateNewBlockHandler(ILogger<CreateNewBlockHandler> logger,
                                     IMessageBroker messageBroker,
                                     IHasher hasher,
                                     IBlockConfirmationPolicy blockConfirmationPolicy,
                                     IBlockCreationPolicy blockCreationPolicy,
                                     IBlockRepository blockRepository,
                                     IEntryRepository entryRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _blockConfirmationPolicy = blockConfirmationPolicy ?? throw new ArgumentNullException(nameof(blockConfirmationPolicy));
            _blockCreationPolicy = blockCreationPolicy ?? throw new ArgumentNullException(nameof(blockCreationPolicy));
            _blockRepository = blockRepository ?? throw new ArgumentNullException(nameof(blockRepository));
            _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
        }
        public async Task HandleAsync(CreateNewBlock command, CancellationToken cancellationToken = default)
        {
            if (!await _blockCreationPolicy.CanCreateNewBlockAsync())
            {
                return;
            }


            var previousBlock = await _blockRepository.GetLastAsync();

            if (!_blockConfirmationPolicy.BlockWasConfirmed(previousBlock))
            {
                _logger.LogInformation($"Block with ID {previousBlock.BusinessId} was not confirmed.");
                await _messageBroker.PublishAsync(new BlockAdded(previousBlock.BusinessId,previousBlock.Confirmation,_blockConfirmationPolicy.BlockRequiredConfirmation), cancellationToken);
                return;
            }

            var previousBlockHash = previousBlock is not null ? _hasher.Hash(previousBlock.MapToDto()) : null;

            var entriesToProceed = await _entryRepository.GetWithoutBlock(3);

            var newBlock = new Block(previousBlock?.BusinessId, previousBlockHash);
            newBlock.AddEntries(entriesToProceed, Array.Empty<byte>());

            await _blockRepository.AddAsync(newBlock);

            await _messageBroker.PublishAsync(new BlockAdded(newBlock.BusinessId,0,_blockConfirmationPolicy.BlockRequiredConfirmation), cancellationToken);
            _logger.LogInformation($"Block with ID {newBlock.BusinessId} was added.");
        }
    }
}
