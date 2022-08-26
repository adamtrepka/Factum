using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Entries.DTO
{
    internal class EntryDto
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public byte[] FileHash { get; set; }
    }
}
