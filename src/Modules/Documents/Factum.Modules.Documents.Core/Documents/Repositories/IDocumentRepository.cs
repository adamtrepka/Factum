using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Core.Documents.Repositories
{
    internal interface IDocumentRepository
    {
        Task<Document> GetAsync(DocumentId id);
        Task AddAsync(Document wallet);
        Task UpdateAsync(Document document);
    }
}
