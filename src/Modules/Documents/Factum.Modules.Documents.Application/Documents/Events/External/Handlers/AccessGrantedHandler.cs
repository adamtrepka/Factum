using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Events.External.Handlers
{
    internal class AccessGrantedHandler : IEventHandler<AccessGranted>
    {
        private readonly IDocumentRepository _documentRepository;

        public AccessGrantedHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }
        public async Task HandleAsync(AccessGranted @event, CancellationToken cancellationToken = default)
        {
            var document = await _documentRepository.GetAsync(@event.DocumentId);
            document.AddEntitlement(@event.GrantedTo);
            await _documentRepository.UpdateAsync(document);
        }
    }
}
