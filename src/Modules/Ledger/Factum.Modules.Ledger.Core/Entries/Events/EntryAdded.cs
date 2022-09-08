using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Entries.Events
{
    internal record EntryAdded() : IDomainEvent;
}
