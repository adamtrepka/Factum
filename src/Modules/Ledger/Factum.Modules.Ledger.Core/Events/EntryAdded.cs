﻿using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Events
{
    internal record EntryAdded(Guid EntryId) : IEvent;
}