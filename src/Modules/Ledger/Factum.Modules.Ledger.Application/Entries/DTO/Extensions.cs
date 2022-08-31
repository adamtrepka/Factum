using Factum.Modules.Ledger.Core.Entries.Entities;

namespace Factum.Modules.Ledger.Application.Entries.DTO
{
    internal static class Extensions
    {
        public static EntryDto MapToDto(this Entry entry)
        {
            return new EntryDto()
            {
                Id = entry.BlockId,
                Metadata = entry.Metadata.ToDictionary(x => x.Key, x => x.Value),
                MetadataHash = entry.MetadataHash
            };
        }
    }
}
