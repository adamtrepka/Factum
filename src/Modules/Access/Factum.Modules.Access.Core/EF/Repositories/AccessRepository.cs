using Factum.Modules.Access.Core.Repositories;
using Factum.Modules.Access.Core.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Access.Core.EF.Repositories
{
    internal class AccessRepository : IAccessRepository
    {
        private readonly AccessDbContext _dbContext;
        private readonly DbSet<Entities.Access> _access;

        public AccessRepository(AccessDbContext dbContext)
        {
            _dbContext = dbContext;
            _access = dbContext.Accesses;
        }

        public Task<Entities.Access> GetAsync(DocumentId documentId, UserId grantedTo)
            => _access.SingleOrDefaultAsync(x => x.DocumentId == documentId && x.GrantedTo == grantedTo);

        public async Task<IReadOnlyList<Entities.Access>> GetGrantedAccesses(DocumentId documentId, UserId grantedBy)
            => await _access.Where(x => x.DocumentId == documentId && x.GrantedBy == grantedBy).ToListAsync();

        public async Task AddAsync(Entities.Access access)
        {
            await _access.AddAsync(access);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var access = await _access.FirstOrDefaultAsync(x => x.Id == id);
            _access.Remove(access);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid businessId)
        {
            var access = await _access.FirstOrDefaultAsync(x => x.BusinessId == businessId);
            _access.Remove(access);
            await _dbContext.SaveChangesAsync();
        }
    }
}
