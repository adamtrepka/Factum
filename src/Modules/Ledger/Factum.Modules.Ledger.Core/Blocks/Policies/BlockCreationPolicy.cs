using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Modules.Ledger.Core.Entries.Repositories;

namespace Factum.Modules.Ledger.Core.Blocks.Policies
{
    internal interface IBlockCreationPolicy
    {
        Task<bool> CanCreateNewBlockAsync();
    }

    internal class BlockCreationPolicy : IBlockCreationPolicy
    {
        private readonly int _requiredNumberOfEntriesToCreateNewBlock = 4;
        private readonly IEntryRepository _entryRepository;

        public BlockCreationPolicy(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
        }

        public async Task<bool> CanCreateNewBlockAsync()
        {
            int numberOfEntries = await _entryRepository.CountWaitingToBeProcessed();
            return numberOfEntries >= _requiredNumberOfEntriesToCreateNewBlock;
        }
    }
}
