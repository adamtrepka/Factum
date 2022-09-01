using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Shared.Abstractions.Kernel.Exceptions;

public class InvalidEmailException : FactumException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}