using Factum.Modules.Users.Core.DTO;
using Factum.Shared.Abstractions.Queries;

namespace Factum.Modules.Users.Core.Queries;

internal class GetUserByEmail : IQuery<UserDetailsDto>
{
    public string Email { get; set; }
}