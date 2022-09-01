using System;
using System.ComponentModel.DataAnnotations;
using Factum.Shared.Abstractions.Commands;

namespace Factum.Modules.Users.Core.Commands;

internal record SignUp([Required][EmailAddress] string Email, [Required] string Password, string Role) : ICommand
{
    public Guid UserId { get; init; } = Guid.NewGuid();
}