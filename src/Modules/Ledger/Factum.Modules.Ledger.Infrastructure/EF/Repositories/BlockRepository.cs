using Factum.Modules.Ledger.Core.Blocks.Entities;
using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Modules.Ledger.Core.Blocks.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Infrastructure.EF.Repositories
{
    internal class BlockRepository : IBlockRepository
    {
        private readonly LedgerDbContext _ledgerDbContext;
        private readonly DbSet<Block> _blocks;

        public BlockRepository(LedgerDbContext ledgerDbContext)
        {
            _ledgerDbContext = ledgerDbContext;
            _blocks = _ledgerDbContext.Blockchain;
        }
        public async Task AddAsync(Block block)
        {
            await _blocks.AddAsync(block);
            await _ledgerDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(BlockId id)
        {
            var block = await GetAsync(id);
            _blocks.Remove(block);
            await _ledgerDbContext.SaveChangesAsync();
        }

        public async Task<Block> GetAsync(BlockId id)
        {
            return await _blocks.Include(x => x.Entries).SingleOrDefaultAsync(x => x.BusinessId == id);
        }

        public async Task<Block> GetLastAsync()
        {
           return await _blocks.Include(x => x.Entries).OrderBy(x => x.Id).LastOrDefaultAsync();
        }

        public async Task UpdateAsync(Block block)
        {
            _blocks.Update(block);
            await _ledgerDbContext.SaveChangesAsync();
        }
    }
}
