using Factum.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Events
{
    internal record DocumentAdded(Guid documentId) : IEvent;
}
