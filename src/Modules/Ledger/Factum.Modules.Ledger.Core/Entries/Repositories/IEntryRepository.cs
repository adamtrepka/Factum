using Factum.Modules.Ledger.Core.Blocks.Types;
using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Modules.Ledger.Core.Entries.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Entries.Repositories
{
    internal interface IEntryRepository
    {
        Task<Entry> GetAsync(EntryId entryId);
        Task<List<Entry>> GetAsync(BlockId blockId);
        Task AddAsync(Entry entry);
        Task<List<Entry>> GetWithoutBlock(int take = 3);
        Task<int> CountWaitingToBeProcessed();
    }
}
