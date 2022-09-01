using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Users.Core.Exceptions;

internal class InvalidEmailException : FactumException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"State is invalid: '{email}'.")
    {
        Email = email;
    }
}