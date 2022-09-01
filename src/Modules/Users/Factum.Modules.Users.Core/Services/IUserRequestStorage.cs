using System;
using Factum.Shared.Abstractions.Auth;

namespace Factum.Modules.Users.Core.Services;

internal interface IUserRequestStorage
{
    void SetToken(Guid commandId, JsonWebToken jwt);
    JsonWebToken GetToken(Guid commandId);
}