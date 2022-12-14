using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Entries.Events.External
{
    internal record AccessRevoked(Guid DocumentId, Guid RevokedBy, Guid RevokedTo) : IEvent;

    [Message("access")]
    internal class AccessRevokedContract : Contract<AccessRevoked>
    {
        public AccessRevokedContract()
        {
            RequireAll();
        }
    }
}
