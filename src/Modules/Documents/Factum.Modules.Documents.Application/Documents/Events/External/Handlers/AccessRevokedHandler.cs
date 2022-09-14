using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Shared.Abstractions.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Events.External.Handlers
{
    internal class AccessRevokedHandler : IEventHandler<AccessRevoked>
    {
        private readonly IDocumentRepository _documentRepository;

        public AccessRevokedHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }

        public async Task HandleAsync(AccessRevoked @event, CancellationToken cancellationToken = default)
        {
            var document = await _documentRepository.GetAsync(@event.DocumentId);
            document.RemoveEntitlement(@event.RevokedTo);
            await _documentRepository.UpdateAsync(document);
        }
    }
}
