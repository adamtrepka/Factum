using Factum.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Validation.Core.Events
{
    internal record BlockValidated(Guid BlockId) : IEvent;

}
