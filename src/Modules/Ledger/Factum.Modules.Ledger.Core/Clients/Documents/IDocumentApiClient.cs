using Factum.Modules.Ledger.Core.Clients.Documents.Dto;

namespace Factum.Modules.Ledger.Core.Clients.Documents
{
    internal interface IDocumentApiClient
    {
        Task<DocumentDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}