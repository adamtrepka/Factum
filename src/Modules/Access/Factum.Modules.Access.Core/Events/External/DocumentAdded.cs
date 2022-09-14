using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Access.Core.Events.External
{
    internal record DocumentAdded(Guid documentId, Guid addedBy) : IEvent;

    [Message("documents")]
    internal class DocumentAddedContract : Contract<DocumentAdded>
    {
        public DocumentAddedContract()
        {
            RequireAll();
        }
    }
}
