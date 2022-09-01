using System;
using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Users.Core.Events;

internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;