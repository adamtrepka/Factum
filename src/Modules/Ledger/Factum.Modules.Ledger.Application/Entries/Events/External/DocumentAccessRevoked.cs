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
    internal record DocumentAccessRevoked(Guid AccessId, Guid DocumentId, string AccessType, Guid RevokedBy, Guid RevokedTo) : IEvent;

    [Message("documents")]
    internal class DocumentAccessRevokedContract : Contract<DocumentAccessRevoked>
    {
        public DocumentAccessRevokedContract()
        {
            RequireAll();
        }
    }
}
