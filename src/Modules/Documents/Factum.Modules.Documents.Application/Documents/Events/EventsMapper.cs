using Factum.Modules.Documents.Core.Documents.Events;
using Factum.Shared.Abstractions.Kernel;
using Factum.Shared.Abstractions.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace Factum.Modules.Documents.Application.Documents.Events
{
    internal class EventMapper : IEventMapper
    {
        public IMessage Map(IDomainEvent @event)
            => @event switch
            {
                DocumentCreated e => new DocumentAdded(e.Document.BusinessId),
                AccessGranted e => new DocumentAccessGranted(e.Access.BusinessId, e.Access.DocumentId, e.Access.AccessType, e.Access.GrantedBy, e.Access.GrantedTo),
                AccessRevoked e => new DocumentAccessRevoked(e.Access.BusinessId, e.Access.DocumentId, e.Access.AccessType, e.RevokedBy, e.Access.GrantedTo),
                _ => null
            };

        public IMessage[] MapAll(IEnumerable<IDomainEvent> events) 
            => events.Select(Map).ToArray();

    }
}
