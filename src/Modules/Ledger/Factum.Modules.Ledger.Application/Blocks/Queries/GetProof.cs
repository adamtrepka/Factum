using Factum.Modules.Ledger.Application.Blocks.DTO;
using Factum.Shared.Abstractions.Queries;

namespace Factum.Modules.Ledger.Application.Blocks.Queries
{
    internal class GetProof : IQuery<ProofDto>
    {
        public GetProof(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
