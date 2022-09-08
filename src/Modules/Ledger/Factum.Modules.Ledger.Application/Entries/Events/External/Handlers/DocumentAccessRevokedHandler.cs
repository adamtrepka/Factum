﻿using Factum.Modules.Ledger.Application.Blocks.Commands;
using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Modules.Ledger.Core.Entries.Events;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace Factum.Modules.Ledger.Application.Entries.Events.External.Handlers
{
    internal class DocumentAccessRevokedHandler : IEventHandler<DocumentAccessRevoked>
    {
        private readonly ILogger<DocumentAccessGrantedHandler> _logger;
        private readonly IEntryRepository _entryRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public DocumentAccessRevokedHandler(ILogger<DocumentAccessGrantedHandler> logger, IEntryRepository entryRepository, IDomainEventDispatcher domainEventDispatcher)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
            _domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
        }
        public async Task HandleAsync(DocumentAccessRevoked @event, CancellationToken cancellationToken = default)
        {
            var metadata = new Dictionary<string, string>()
            {
                {"Type",nameof(DocumentAccessRevoked)},
                {nameof(@event.AccessId),@event.AccessId.ToString()},
                {nameof(@event.DocumentId),@event.DocumentId.ToString()},
                {nameof(@event.AccessType),@event.AccessType.ToString()},
                {nameof(@event.RevokedBy),@event.RevokedBy.ToString()},
                {nameof(@event.RevokedTo),@event.RevokedTo.ToString()}
            };

            var entry = new Entry(@event.AccessId, metadata);

            await _entryRepository.AddAsync(entry);

            _logger.LogInformation($"Created a new entry based on granted access with ID: '{@event.AccessId}'.");

            await _domainEventDispatcher.DispatchAsync(new EntryAdded(), cancellationToken);
        }
    }
}
