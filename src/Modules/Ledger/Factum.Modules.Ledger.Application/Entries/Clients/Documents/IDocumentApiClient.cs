using Factum.Modules.Ledger.Application.Entries.Clients.Documents.Dto;

namespace Factum.Modules.Ledger.Application.Entries.Clients.Documents
{
    internal interface IDocumentApiClient
    {
        Task<DocumentDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}