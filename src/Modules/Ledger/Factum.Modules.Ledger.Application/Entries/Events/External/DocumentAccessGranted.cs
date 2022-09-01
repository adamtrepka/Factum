using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;

namespace Factum.Modules.Ledger.Application.Entries.Events.External
{
    internal record DocumentAccessGranted(Guid AccessId, Guid DocumentId, string AccessType, Guid GrantedBy, Guid GrantedTo) : IEvent;

    [Message("documents")]
    internal class DocumentAccessGrantedContract : Contract<DocumentAccessGranted>
    {
        public DocumentAccessGrantedContract()
        {
            RequireAll();
        }
    }
}
