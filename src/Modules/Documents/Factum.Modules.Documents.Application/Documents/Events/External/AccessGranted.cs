using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Events.External
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
