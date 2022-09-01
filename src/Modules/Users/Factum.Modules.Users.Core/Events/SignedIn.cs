using System;
using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Users.Core.Events;

internal record SignedIn(Guid UserId) : IEvent;