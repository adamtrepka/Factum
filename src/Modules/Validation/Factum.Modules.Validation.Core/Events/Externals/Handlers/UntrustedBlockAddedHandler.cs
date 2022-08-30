﻿using Factum.Modules.Validation.Core.Clients.Ledger;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;
using Factum.Shared.Infrastructure.Security.Encryption;
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

        public UntrustedBlockAddedHandler(IHasher hasher, ILedgerApiClient ledgerApiClient, IMessageBroker messageBroker)
        {
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _ledgerApiClient = ledgerApiClient ?? throw new ArgumentNullException(nameof(ledgerApiClient));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
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

            if (block.PreviousBlockId.HasValue)
            {
                var previousBlock = await _ledgerApiClient.GetAsync(block.PreviousBlockId.Value, cancellationToken);
                previousBlockTestResult = _hasher.Validate(previousBlock, block.PreviousBlockHash);
            }

            metadataHashTestResult = block.Entries.All(x => _hasher.Validate(x.Metadata, x.MetadataHash));

            if (previousBlockTestResult && metadataHashTestResult is true)
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
