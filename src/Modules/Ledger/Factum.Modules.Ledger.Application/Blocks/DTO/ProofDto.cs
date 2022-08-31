using Factum.Modules.Ledger.Application.Entries.DTO;

namespace Factum.Modules.Ledger.Application.Blocks.DTO
{
    internal class ProofDto
    {
        public BlockDetailsDto Block { get; set; }
        public bool IsValid { get; set; }
    }
}
