using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;

namespace Factum.Modules.Documents.Core.Documents.Repositories
{
    internal interface IDocumentRepository
    {
        Task<Document> GetAsync(DocumentId id);
        Task AddAsync(Document wallet);
    }
}
