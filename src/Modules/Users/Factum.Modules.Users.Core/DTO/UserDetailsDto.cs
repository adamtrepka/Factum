using System.Collections.Generic;

namespace Factum.Modules.Users.Core.DTO;

public class UserDetailsDto : UserDto
{
    public IEnumerable<string> Permissions { get; set; }
}