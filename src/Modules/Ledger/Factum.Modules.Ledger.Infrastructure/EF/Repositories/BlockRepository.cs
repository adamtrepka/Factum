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
            var block = await _blocks.IgnoreAutoIncludes().FirstOrDefaultAsync(x => x.BusinessId == id);
            _blocks.Remove(block);
            await _ledgerDbContext.SaveChangesAsync();
        }

        public Task<Block> GetAsync(BlockId id)
        {
            return _blocks.Include(x => x.Entries).ThenInclude(x => x.Metadata).SingleOrDefaultAsync(x => x.BusinessId == id);
        }

        public async Task<Block> GetLastAsync()
        {
            var last = await _blocks.Include(x => x.Entries).ThenInclude(x => x.Metadata).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (last is null && await _blocks.AnyAsync())
                throw new Exception("Unable to get last block");

            return last;
        }

        public async Task UpdateAsync(Block block)
        {
            _blocks.Update(block);
            await _ledgerDbContext.SaveChangesAsync();
        }
    }
}
