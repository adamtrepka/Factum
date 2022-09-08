using Factum.Modules.Ledger.Application.Blocks.Commands;
using Factum.Modules.Ledger.Application.Blocks.Events;
using Factum.Modules.Ledger.Core.Blocks.Policies;
using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Modules.Ledger.Core.Entries.Events;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Modules.Ledger.Infrastructure.Clients.Saga;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Abstractions.Kernel;
using Factum.Shared.Abstractions.Messaging;

namespace Factum.Modules.Ledger.Application.Entries.Events.Handlers
{
    internal class EntryAddedHandler : IDomainEventHandler<EntryAdded>
    {
        private readonly IDispatcher _dispatcher;
        private readonly IBlockRepository _blockRepository;
        private readonly IEntryRepository _entryRepository;
        private readonly IBlockCreationPolicy _blockCreationPolicy;
        private readonly IBlockConfirmationPolicy _blockConfirmationPolicy;
        private readonly IBlockSagaStatusPolicy _blockSagaStatusPolicy;
        private readonly IMessageBroker _messageBroker;
        private readonly ISagaApiClient _sagaApiClient;

        public EntryAddedHandler(IDispatcher dispatcher,
                                 IBlockRepository blockRepository,
                                 IEntryRepository entryRepository,
                                 IBlockCreationPolicy blockCreationPolicy,
                                 IBlockConfirmationPolicy blockConfirmationPolicy,
                                 IBlockSagaStatusPolicy blockSagaStatusPolicy,
                                 IMessageBroker messageBroker)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _blockRepository = blockRepository;
            _entryRepository = entryRepository;
            _blockCreationPolicy = blockCreationPolicy;
            _blockConfirmationPolicy = blockConfirmationPolicy;
            _blockSagaStatusPolicy = blockSagaStatusPolicy;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(EntryAdded @event, CancellationToken cancellationToken = default)
        {
            if (!await _blockCreationPolicy.CanCreateNewBlockAsync())
            {
                return;
            }

            var previousBlock = await _blockRepository.GetLastAsync();

            if (await _blockSagaStatusPolicy.IsPending(previousBlock))
            {
                return;
            }

            if(_blockConfirmationPolicy.BlockWasConfirmed(previousBlock) is false)
            {
                await _messageBroker.PublishAsync(new BlockAdded(previousBlock.BusinessId, previousBlock.Confirmation, _blockConfirmationPolicy.BlockRequiredConfirmation), cancellationToken);
                return;
            }

            await _dispatcher.SendAsync(new CreateNewBlock(previousBlock));
        }
    }
}
