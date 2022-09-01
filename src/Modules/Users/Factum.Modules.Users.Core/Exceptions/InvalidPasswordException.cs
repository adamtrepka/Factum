
using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Users.Core.Exceptions;

internal class InvalidPasswordException : FactumException
{
    public string Reason { get; }

    public InvalidPasswordException(string reason) : base($"Invalid password: {reason}.")
    {
        Reason = reason;
    }
}