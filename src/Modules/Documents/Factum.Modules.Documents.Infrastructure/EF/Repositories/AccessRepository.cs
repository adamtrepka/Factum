using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Modules.Documents.Core.Documents.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Infrastructure.EF.Repositories
{
    internal class AccessRepository : IAccessRepository
    {
        private readonly DocumentsDbContext _dbContext;
        private readonly DbSet<Access> _accesses;

        public AccessRepository(DocumentsDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _accesses = dbContext.Accesses;
        }
        public Task<bool> HasAccessAsync(DocumentId id, UserId userId)
            => _accesses.AnyAsync(x => x.DocumentId == id && x.GrantedTo == userId);

        public Task<Access> GetAccessAsync(DocumentId id, UserId userId)
            => _accesses.FirstOrDefaultAsync(x => x.DocumentId == id && x.GrantedTo == userId);
    }
}
