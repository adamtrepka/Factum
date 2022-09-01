using System;
using Factum.Shared.Abstractions.Commands;

namespace Factum.Modules.Users.Core.Commands;

internal record SignOut(Guid UserId) : ICommand;