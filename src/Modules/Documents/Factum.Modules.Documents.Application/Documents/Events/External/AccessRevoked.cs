using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;
using System;
using System.Diagnostics.Contracts;

namespace Factum.Modules.Documents.Application.Documents.Events.External
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
