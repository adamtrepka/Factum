using Factum.Modules.Ledger.Application.Blocks.Commands;
using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Factum.Modules.Ledger.Application.Entries.Events.External.Handlers
{
    internal class AccessGrantedHandler : IEventHandler<DocumentAccessGranted>
    {
        private readonly ILogger<AccessGrantedHandler> _logger;
        private readonly IEntryRepository _entryRepository;
        private readonly IDispatcher _dispatcher;

        public AccessGrantedHandler(ILogger<AccessGrantedHandler> logger, IEntryRepository entryRepository, IDispatcher dispatcher)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }
        public async Task HandleAsync(DocumentAccessGranted @event, CancellationToken cancellationToken = default)
        {
            var metadata = new Dictionary<string, string>()
            {
                {"Type",nameof(DocumentAccessGranted)},
                {nameof(@event.AccessId),@event.AccessId.ToString()},
                {nameof(@event.DocumentId),@event.DocumentId.ToString()},
                {nameof(@event.AccessType),@event.AccessType.ToString()},
                {nameof(@event.GrantedBy),@event.GrantedBy.ToString()},
                {nameof(@event.GrantedTo),@event.GrantedTo.ToString()}
            };

            var entry = new Entry(@event.AccessId, metadata);

            await _entryRepository.AddAsync(entry);

            _logger.LogInformation($"Created a new entry based on granted access with ID: '{@event.AccessId}'.");

            await _dispatcher.SendAsync(new CreateNewBlock(), cancellationToken);
        }
    }
}
