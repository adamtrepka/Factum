using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Users.Core.Exceptions;

internal class EmailInUseException : FactumException
{
    public EmailInUseException() : base("Email is already in use.")
    {
    }
}