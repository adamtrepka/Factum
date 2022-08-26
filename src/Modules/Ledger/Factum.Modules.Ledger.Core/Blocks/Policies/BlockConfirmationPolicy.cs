using Factum.Modules.Ledger.Core.Blocks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Blocks.Policies
{
    internal interface IBlockConfirmationPolicy
    {
        int BlockRequiredConfirmation { get; }
        bool BlockWasConfirmed(Block block);
    }

    internal class BlockConfirmationPolicy : IBlockConfirmationPolicy
    {
        public int BlockRequiredConfirmation { get; } = 3;
        public bool BlockWasConfirmed(Block block)
        {
            if (block is null) return true;
            return block.Confirmation >= BlockRequiredConfirmation;
        }
    }
}
