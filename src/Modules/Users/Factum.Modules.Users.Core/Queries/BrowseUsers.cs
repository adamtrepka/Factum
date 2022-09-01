using Factum.Modules.Users.Core.DTO;
using Factum.Shared.Abstractions.Queries;

namespace Factum.Modules.Users.Core.Queries;

internal class BrowseUsers : PagedQuery<UserDto>
{
    public string Email { get; set; }
    public string Role { get; set; }
    public string State { get; set; }
}