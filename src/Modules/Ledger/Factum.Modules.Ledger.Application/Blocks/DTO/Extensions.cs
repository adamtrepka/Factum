using Factum.Modules.Ledger.Application.Entries.DTO;
using Factum.Modules.Ledger.Core.Blocks.Entities;

namespace Factum.Modules.Ledger.Application.Blocks.DTO
{
    internal static class Extensions
    {
        public static IEnumerable<BlockDto> MapToDto(this IEnumerable<Block> blocks)
        {
            return blocks.Select(x => new BlockDto
            {
                Id = x.BusinessId,
                PreviousBlockId = x.PreviousBlockId?.Value,
                PreviousBlockHash = x.PreviousBlockHash,
                EntriesRootHash = x.EntriesRootHash,
                Confirmations = x.Confirmation
            });
        }

        public static BlockDetailsDto MapToDto(this Block block)
        {
            return new BlockDetailsDto
            {
                Id = block.BusinessId,
                PreviousBlockId = block.PreviousBlockId?.Value,
                PreviousBlockHash = block.PreviousBlockHash,
                EntriesRootHash = block.EntriesRootHash,
                Confirmations = block.Confirmation,
                Entries = block?.Entries?.Select(x => new EntryDto()
                {
                    Id = x.BusinessId,
                    DocumentId = x.DocumentId,
                    FileHash = x.FileHash,
                }).ToList().AsReadOnly()
            };
        }
    }
}
