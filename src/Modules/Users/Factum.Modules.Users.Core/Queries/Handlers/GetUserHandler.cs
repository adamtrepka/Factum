using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Factum.Shared.Abstractions.Queries;
using Factum.Modules.Users.Core.Queries;
using Factum.Modules.Users.Core.DTO;
using Factum.Modules.Users.Core.EF;

namespace Factum.Modules.Users.Core.Queries.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDetailsDto>
{
    private readonly UsersDbContext _dbContext;

    public GetUserHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDetailsDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == query.UserId, cancellationToken);

        return user?.AsDetailsDto();
    }
}