using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;

namespace Factum.Modules.Ledger.Application.Entries.Events.External
{
    internal record AccessGranted(Guid DocumentId, Guid GrantedBy, Guid GrantedTo) : IEvent;

    [Message("access")]
    internal class AccessGrantedContract : Contract<AccessGranted>
    {
        public AccessGrantedContract()
        {
            RequireAll();
        }
    }
}
