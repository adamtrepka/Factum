using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Users.Core.Exceptions;

internal class InvalidCredentialsException : FactumException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}