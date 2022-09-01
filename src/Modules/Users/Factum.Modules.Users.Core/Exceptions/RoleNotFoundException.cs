using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Users.Core.Exceptions;

internal class RoleNotFoundException : FactumException
{
    public RoleNotFoundException(string role) : base($"Role: '{role}' was not found.")
    {
    }
}