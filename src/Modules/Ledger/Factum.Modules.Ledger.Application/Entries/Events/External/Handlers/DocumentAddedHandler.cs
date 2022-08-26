using Factum.Modules.Ledger.Application.Blocks.Commands;
using Factum.Modules.Ledger.Application.Entries.Clients.Documents;
using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Factum.Modules.Ledger.Application.Entries.Events.External.Handlers
{
    internal class DocumentAddedHandler : IEventHandler<DocumentAdded>
    {
        private readonly IDocumentApiClient _documentApiClient;
        private readonly ILogger<DocumentAddedHandler> _logger;
        private readonly IDispatcher _dispatcher;
        private readonly IEntryRepository _entryRepository;

        public DocumentAddedHandler(IDocumentApiClient documentApiClient,
                                    ILogger<DocumentAddedHandler> logger,
                                    IDispatcher dispatcher,
                                    IEntryRepository entryRepository)
        {
            _documentApiClient = documentApiClient ?? throw new ArgumentNullException(nameof(documentApiClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
        }
        public async Task HandleAsync(DocumentAdded @event, CancellationToken cancellationToken = default)
        {
            var document = await _documentApiClient.GetAsync(@event.documentId, cancellationToken);
            var entry = new Entry(document.DocumentId, document.FileHash);

            await _entryRepository.AddAsync(entry);

            _logger.LogInformation($"Created a new entry based on document with ID: '{@event.documentId}'.");

            await _dispatcher.SendAsync(new CreateNewBlock(), cancellationToken);
        }
    }
}
