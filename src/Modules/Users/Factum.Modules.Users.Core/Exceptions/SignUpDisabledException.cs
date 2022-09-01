using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Users.Core.Exceptions;

internal class SignUpDisabledException : FactumException
{
    public SignUpDisabledException() : base("Sign up is disabled.")
    {
    }
}