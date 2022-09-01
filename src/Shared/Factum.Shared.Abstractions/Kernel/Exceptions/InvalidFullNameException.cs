using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Shared.Abstractions.Kernel.Exceptions;

public class InvalidFullNameException : FactumException
{
    public string FullName { get; }

    public InvalidFullNameException(string fullName) : base($"Full name: '{fullName}' is invalid.")
    {
        FullName = fullName;
    }
}