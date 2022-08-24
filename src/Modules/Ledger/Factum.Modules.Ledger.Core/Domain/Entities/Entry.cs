using Factum.Modules.Ledger.Core.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Domain.Entities
{
    internal class Entry
    {
        private Entry()
        {

        }
        public Entry(Guid documentId, byte[] fileHash) : this(new EntryId(), documentId, fileHash)
        {

        }
        public Entry(EntryId businessId, Guid documentId, byte[] fileHash)
        {
            BusinessId = businessId;
            DocumentId = documentId;
            FileHash = fileHash;
        }

        public int Id { get; private set; }
        public EntryId BusinessId { get; private set; }
        public Guid DocumentId { get; private set; }
        public byte[] FileHash { get; private set; }

    }
}