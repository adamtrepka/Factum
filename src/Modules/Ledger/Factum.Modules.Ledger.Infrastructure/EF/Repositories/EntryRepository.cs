using Factum.Modules.Ledger.Core.Blocks.Types;
using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Modules.Ledger.Core.Entries.Types;
using Microsoft.EntityFrameworkCore;

namespace Factum.Modules.Ledger.Infrastructure.EF.Repositories
{
    internal class EntryRepository : IEntryRepository
    {
        private readonly LedgerDbContext _ledgerDbContext;
        private readonly DbSet<Entry> _entries;

        public EntryRepository(LedgerDbContext ledgerDbContext)
        {
            _ledgerDbContext = ledgerDbContext;
            _entries = _ledgerDbContext.Entries;
        }
        public async Task AddAsync(Entry entry)
        {
            await _entries.AddAsync(entry);
            await _ledgerDbContext.SaveChangesAsync();
        }

        public async Task<int> CountWaitingToBeProcessed()
        {
            return await _entries.CountAsync(x => x.BlockId == null);
        }

        public async Task<Entry> GetAsync(EntryId entryId)
        {
            return await _entries.SingleOrDefaultAsync(x => x.BusinessId == entryId);
        }

        public async Task<List<Entry>> GetAsync(BlockId blockId)
        {
            return await _entries.Where(x => x.BlockId == blockId).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<List<Entry>> GetWithoutBlock(int take = 3)
        {
            return await _entries.Where(x => x.BlockId == null).OrderBy(x => x.Id).Take(take).ToListAsync();
        }
    }
}
