using Factum.Modules.Ledger.Core.Entries.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Entries.Entities
{
    internal class EntryMetadata
    {
        public int Id { get; private set; }
        public EntryId EntryId { get; private set; }
        public Entry Entry { get; set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        private EntryMetadata()
        {

        }

        public EntryMetadata(EntryId entryId, string key, string value)
        {
            EntryId = entryId;
            Key = key;
            Value = value;
        }
    }
}
