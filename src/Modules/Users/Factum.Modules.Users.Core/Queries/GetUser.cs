using System;
using Factum.Modules.Users.Core.DTO;
using Factum.Shared.Abstractions.Queries;

namespace Factum.Modules.Users.Core.Queries;

internal class GetUser : IQuery<UserDetailsDto>
{
    public Guid UserId { get; set; }
}