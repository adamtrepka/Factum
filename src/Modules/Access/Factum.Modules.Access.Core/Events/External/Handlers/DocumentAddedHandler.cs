using Factum.Modules.Access.Core.Repositories;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Access.Core.Events.External.Handlers
{
    internal class DocumentAddedHandler : IEventHandler<DocumentAdded>
    {
        private readonly IAccessRepository _accessRepository;
        private readonly IMessageBroker _messageBroker;

        public DocumentAddedHandler(IAccessRepository accessRepository, IMessageBroker messageBroker)
        {
            _accessRepository = accessRepository ?? throw new ArgumentNullException(nameof(accessRepository));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
        }
        public async Task HandleAsync(DocumentAdded @event, CancellationToken cancellationToken = default)
        {
            var newAccess = new Entities.Access(@event.documentId, new Types.AccessType("owner"), @event.addedBy, @event.addedBy);

            await _accessRepository.AddAsync(newAccess);
            await _messageBroker.PublishAsync(new AccessGranted(@event.documentId, @event.addedBy, @event.addedBy),cancellationToken);
        }
    }
}
