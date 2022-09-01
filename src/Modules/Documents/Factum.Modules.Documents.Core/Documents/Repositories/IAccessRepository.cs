using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Core.Documents.Repositories
{
    internal interface IAccessRepository
    {
        Task<Access> GetAccessAsync(DocumentId id, UserId userId);
        Task<bool> HasAccessAsync(DocumentId id, UserId userId);
    }
}
