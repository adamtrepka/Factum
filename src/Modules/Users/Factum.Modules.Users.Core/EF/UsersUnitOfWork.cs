using Factum.Shared.Infrastructure.SqlServer;

namespace Factum.Modules.Users.Core.EF;

internal class UsersUnitOfWork : SqlServerUnitOfWork<UsersDbContext>
{
    public UsersUnitOfWork(UsersDbContext dbContext) : base(dbContext)
    {
    }
}