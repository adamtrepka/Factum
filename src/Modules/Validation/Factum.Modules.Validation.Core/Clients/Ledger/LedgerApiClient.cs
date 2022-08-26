using Factum.Modules.Validation.Core.Clients.Ledger.Dto;
using Factum.Shared.Abstractions.Modules;

namespace Factum.Modules.Validation.Core.Clients.Ledger
{
    internal class LedgerApiClient : ILedgerApiClient
    {
        private readonly IModuleClient _moduleClient;

        public LedgerApiClient(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }

        public Task<BlockDetailsDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
            => _moduleClient.SendAsync<BlockDetailsDto>("ledger/get", new { Id = id }, cancellationToken);
    }
}
