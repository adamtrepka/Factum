using Factum.Modules.Ledger.Core.Blocks.Entities;
using Factum.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Blocks.Commands
{
    internal class CreateNewBlock : ICommand
    {
        public CreateNewBlock(Block previousBlock)
        {
            PreviousBlock = previousBlock;
        }

        public Block PreviousBlock { get; }
    }
}
