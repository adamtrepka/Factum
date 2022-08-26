using Factum.Modules.Ledger.Application.Entries.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Blocks.DTO
{
    internal class BlockDto
    {
        public Guid Id { get; set; }
        public Guid? PreviousBlockId { get; set; }
        public byte[] PreviousBlockHash { get; set; }
        public byte[] EntriesRootHash { get; set; }
        public int Confirmations { get; set; }
    }

    internal class BlockDetailsDto : BlockDto
    {
        public IReadOnlyCollection<EntryDto> Entries { get; set; }
    }
}
