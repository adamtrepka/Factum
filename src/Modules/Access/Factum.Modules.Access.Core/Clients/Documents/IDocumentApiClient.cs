using Factum.Modules.Access.Core.Clients.Documents.Dto;

namespace Factum.Modules.Access.Core.Clients.Documents
{
    internal interface IDocumentApiClient
    {
        Task<DocumentDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}