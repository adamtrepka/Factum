using Factum.Modules.Validation.Core.Clients.Ledger.Dto;

namespace Factum.Modules.Validation.Core.Clients.Ledger
{
    internal interface ILedgerApiClient
    {
        Task<BlockDetailsDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}