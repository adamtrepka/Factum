using Factum.Shared.Abstractions.Commands;

namespace Factum.Modules.Users.Core.Commands;

internal record ChangePassword(Guid UserId, string CurrentPassword, string NewPassword) : ICommand;