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
                DocumentCreated e => new DocumentAdded(e.Document.BusinessId,e.CreatedBy),
                _ => null
            };

        public IMessage[] MapAll(IEnumerable<IDomainEvent> events) 
            => events.Select(Map).ToArray();

    }
}
