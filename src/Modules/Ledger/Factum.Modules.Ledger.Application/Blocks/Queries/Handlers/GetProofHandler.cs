using Factum.Modules.Ledger.Application.Blocks.DTO;
using Factum.Modules.Ledger.Application.Entries.DTO;
using Factum.Modules.Ledger.Core.Blocks.Repositories;
using Factum.Modules.Ledger.Core.Entries.Repositories;
using Factum.Modules.Ledger.Core.Entries.Types;
using Factum.Shared.Abstractions.Queries;
using Factum.Shared.Infrastructure.Security.MerkleTree;

namespace Factum.Modules.Ledger.Application.Blocks.Queries.Handlers
{
    internal class GetProofHandler : IQueryHandler<GetProof, ProofDto>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IBlockRepository _blockRepository;
        private readonly IMerkleTree _merkleTree;

        public GetProofHandler(IEntryRepository entryRepository, IBlockRepository blockRepository, IMerkleTree merkleTree)
        {
            _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
            _blockRepository = blockRepository ?? throw new ArgumentNullException(nameof(blockRepository));
            _merkleTree = merkleTree ?? throw new ArgumentNullException(nameof(merkleTree));
        }
        public async Task<ProofDto> HandleAsync(GetProof query, CancellationToken cancellationToken = default)
        {
            var entry = await _entryRepository.GetAsync(new EntryId(query.Id));

            if (entry is null || entry.BlockId is null) return null;

            var block = await _blockRepository.GetAsync(entry.BlockId);

            var merkleTreeResult = _merkleTree.BuildTree(block.Entries.OrderBy(x => x.Id).Select(x => x.MetadataHash));

            var proof = merkleTreeResult.GetProof(entry.MetadataHash);
            var isValid = _merkleTree.Validate(proof, entry.MetadataHash, block.EntriesRootHash);

            var blockDto = block.MapToDto();
            blockDto.Entries = new List<EntryDto>()
            {
                entry.MapToDto()
            };

            return new ProofDto()
            {
                Block = blockDto,
                IsValid = isValid
            };
        }
    }
}
