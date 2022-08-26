using Factum.Modules.Ledger.Core.Blocks.Entities;
using Factum.Modules.Ledger.Core.Blocks.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Blocks.Repositories
{
    internal interface IBlockRepository
    {
        Task<Block> GetAsync(BlockId id);
        Task<Block> GetLastAsync();
        Task AddAsync(Block block);
        Task UpdateAsync(Block block);
        Task DeleteAsync(BlockId id);
    }
}
