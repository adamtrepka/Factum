using Factum.Modules.Ledger.Application.Blocks.Commands;
using Factum.Modules.Ledger.Application.Entries.Clients.Documents;
using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Modules.Ledger.Core.Entries.Events;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Kernel;
using Factum.Shared.Infrastructure.Security.Encryption;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace Factum.Modules.Ledger.Application.Entries.Events.External.Handlers
{
    internal class DocumentAddedHandler : IEventHandler<DocumentAdded>
    {
        private readonly IDocumentApiClient _documentApiClient;
        private readonly ILogger<DocumentAddedHandler> _logger;
        private readonly IEntryRepository _entryRepository;
        private readonly IHasher _hasher;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public DocumentAddedHandler(IDocumentApiClient documentApiClient,
                                    ILogger<DocumentAddedHandler> logger,
                                    IEntryRepository entryRepository,
                                    IHasher hasher,
                                    IDomainEventDispatcher domainEventDispatcher)
        {
            _documentApiClient = documentApiClient ?? throw new ArgumentNullException(nameof(documentApiClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
        }
        public async Task HandleAsync(DocumentAdded @event, CancellationToken cancellationToken = default)
        {
            var document = await _documentApiClient.GetAsync(@event.documentId, cancellationToken);
            var documentMetadata = new Dictionary<string, string>
            {
                {"Type",nameof(DocumentAdded)},
                {nameof(document.DocumentId),document.DocumentId.ToString()},
                {nameof(document.FileName),document.FileName.ToString()},
                {nameof(document.ContentType),document.ContentType},
                {nameof(document.FileHash),_hasher.HashToString(document.FileHash)},
            };
            var entry = new Entry(document.DocumentId, documentMetadata);

            await _entryRepository.AddAsync(entry);

            _logger.LogInformation($"Created a new entry based on document with ID: '{@event.documentId}'.");

            await _domainEventDispatcher.DispatchAsync(new EntryAdded(),cancellationToken);
        }
    }
}
