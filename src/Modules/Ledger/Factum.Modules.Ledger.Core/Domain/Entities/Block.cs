using Factum.Modules.Ledger.Core.Domain.Types;
using Factum.Shared.Abstractions.Kernel.Types;

namespace Factum.Modules.Ledger.Core.Domain.Entities
{
    internal class Block : AggregateRoot<BlockId>
    {
        public BlockId PreviousBlockId { get; private set; }
        public Block PreviousBlock { get; private set; }
        public byte[] PreviousBlockHash { get; private set; }
        public byte[] EntriesRootHash { get; private set; }
        public List<Entry> Entries { get; private set; } = new List<Entry>();

        private Block()
        {

        }

        public Block(BlockId previousBlockId, byte[] previousBlockHash)
        {
            PreviousBlockId = previousBlockId;
            PreviousBlockHash = previousBlockHash;
        }

        public void AddEntries(List<Entry> entries, byte[] entriesRootHash)
        {
            Entries.AddRange(entries);
            EntriesRootHash = entriesRootHash;
        }
    }
}
