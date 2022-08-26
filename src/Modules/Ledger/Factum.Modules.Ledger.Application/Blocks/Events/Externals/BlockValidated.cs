using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Blocks.Events.Externals
{
    internal record BlockValidated(Guid BlockId) : IEvent;

    [Message("Validation")]
    internal class BlockValidatedContract : Contract<BlockValidated>
    {
        public BlockValidatedContract()
        {
            RequireAll();
        }
    }
}
