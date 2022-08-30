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

        public Task<int> CountWaitingToBeProcessed()
        {
            return _entries.CountAsync(x => x.BlockId == null);
        }

        public Task<Entry> GetAsync(EntryId entryId)
        {
            return _entries.SingleAsync(x => x.BusinessId == entryId);
        }

        public Task<List<Entry>> GetAsync(BlockId blockId)
        {
            return _entries.Where(x => x.BlockId == blockId).OrderBy(x => x.Id).ToListAsync();
        }

        public Task<List<Entry>> GetWithoutBlock(int take = 3)
        {
            return _entries.Include(x => x.Metadata).Where(x => x.BlockId == null).OrderBy(x => x.Id).Take(take).ToListAsync();
        }
    }
}
