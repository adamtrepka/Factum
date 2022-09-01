using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Users.Core.Exceptions;

internal class InvalidUserStateException : FactumException
{
    public string State { get; }

    public InvalidUserStateException(string state) : base($"User state is invalid: '{state}'.")
    {
        State = state;
    }
}