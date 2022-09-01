using System;
using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Users.Core.Exceptions;

internal class UserNotActiveException : FactumException
{
    public Guid UserId { get; }

    public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
    {
        UserId = userId;
    }
}