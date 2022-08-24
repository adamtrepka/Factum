using Factum.Modules.Ledger.Core.Clients.Documents;
using Factum.Modules.Ledger.Core.Domain.Entities;
using Factum.Modules.Ledger.Core.EF;
using Factum.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Factum.Modules.Ledger.Core.Events.External.Handlers
{
    internal class DocumentAddedHandler : IEventHandler<DocumentAdded>
    {
        private readonly IDocumentApiClient _documentApiClient;
        private readonly ILogger<DocumentAddedHandler> _logger;
        private readonly LedgerDbContext _dbContext;

        public DocumentAddedHandler(IDocumentApiClient documentApiClient, ILogger<DocumentAddedHandler> logger, LedgerDbContext dbContext)
        {
            _documentApiClient = documentApiClient ?? throw new ArgumentNullException(nameof(documentApiClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task HandleAsync(DocumentAdded @event, CancellationToken cancellationToken = default)
        {
            var document = await _documentApiClient.GetAsync(@event.documentId, cancellationToken);
            var entry = new Entry(document.DocumentId, document.FileHash);

            await _dbContext.Entries.AddAsync(entry,cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Created a new entry based on document with ID: '{@event.documentId}'.");
        }
    }
}
