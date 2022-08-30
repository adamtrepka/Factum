using Factum.Modules.Ledger.Core.Blocks.Entities;
using Factum.Modules.Ledger.Core.Blocks.Types;
using Factum.Modules.Ledger.Core.Entries.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Entries.Entities
{
    internal class Entry 
   
    {
        private Entry()
        {

        }
        public Entry(Dictionary<string, string> metadata) : this(new EntryId(), metadata)
        {

        }
        public Entry(EntryId businessId, Dictionary<string, string> metadata)
        {
            BusinessId = businessId;
            Metadata = metadata.Select(x => new EntryMetadata(businessId, x.Key, x.Value)).ToList();
        }

        public void AttatchToBlock(BlockId blockId, byte[] metadataHash)
        {
            BlockId = blockId;
            MetadataHash = metadataHash;
        }

        public int Id { get; private set; }
        public EntryId BusinessId { get; private set; }
        public List<EntryMetadata> Metadata { get; private set; }
        public byte[] MetadataHash { get; private set; }

        public BlockId BlockId { get; private set; }
        public Block Block { get; private set; }

    }
}