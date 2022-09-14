using Factum.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Access.Core.Events
{
    internal record AccessGranted(Guid DocumentId, Guid GrantedBy, Guid GrantedTo) : IEvent;
}
