using Factum.Modules.Ledger.Core.Clients.Documents;
using Factum.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Events.External.Handlers
{
    internal class DocumentAddedHandler : IEventHandler<DocumentAdded>
    {
        private readonly IDocumentApiClient _documentApiClient;
        private readonly ILogger<DocumentAddedHandler> _logger;

        public DocumentAddedHandler(IDocumentApiClient documentApiClient, ILogger<DocumentAddedHandler> logger)
        {
            _documentApiClient = documentApiClient ?? throw new ArgumentNullException(nameof(documentApiClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task HandleAsync(DocumentAdded @event, CancellationToken cancellationToken = default)
        {
            var document = await _documentApiClient.GetAsync(@event.documentId, cancellationToken);
            _logger.LogInformation($"Created a new hash based on document with ID: '{@event.documentId}'.");
        }
    }
}
