using Factum.Modules.Ledger.Core.Blocks.Types;
using Factum.Modules.Ledger.Core.Entries.Entities;
using Factum.Shared.Abstractions.Kernel.Types;

namespace Factum.Modules.Ledger.Core.Blocks.Entities
{
    internal class Block : AggregateRoot<BlockId>
    {
        public BlockId PreviousBlockId { get; private set; }
        public Block PreviousBlock { get; private set; }
        public byte[] PreviousBlockHash { get; private set; }
        public byte[] EntriesRootHash { get; private set; }
        public virtual List<Entry> Entries { get; private set; } = new List<Entry>();
        public int Confirmation { get; private set; }

        private Block()
        {

        }

        public Block(BlockId previousBlockId, byte[] previousBlockHash)
        {
            BusinessId = new BlockId();
            PreviousBlockId = previousBlockId;
            PreviousBlockHash = previousBlockHash;
        }

        public void AddEntries(List<Entry> entries, byte[] entriesRootHash)
        {
            Entries.AddRange(entries);
            EntriesRootHash = entriesRootHash;
        }

        public void Confirm() => Confirmation++;
    }
}
