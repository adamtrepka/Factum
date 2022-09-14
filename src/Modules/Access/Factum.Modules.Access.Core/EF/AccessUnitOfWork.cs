using Factum.Shared.Infrastructure.SqlServer;

namespace Factum.Modules.Access.Core.EF
{
    internal class AccessUnitOfWork : SqlServerUnitOfWork<AccessDbContext>
    {
        public AccessUnitOfWork(AccessDbContext dbContext) : base(dbContext)
        {
        }
    }
}
