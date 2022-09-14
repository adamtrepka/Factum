using Factum.Modules.Access.Core.Types;

namespace Factum.Modules.Access.Core.Repositories
{
    internal interface IAccessRepository
    {
        Task AddAsync(Entities.Access access);
        Task<Entities.Access> GetAsync(DocumentId documentId, UserId grantedTo);
        Task<IReadOnlyList<Entities.Access>> GetGrantedAccesses(DocumentId documentId, UserId grantedBy);
        Task RemoveAsync(Guid businessId);
        Task RemoveAsync(int id);
    }
}