using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Entries.Clients.Documents.Dto
{
    public class DocumentDto
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileHash { get; set; }
    }
}
