using System;
using System.ComponentModel.DataAnnotations;
using Factum.Shared.Abstractions.Commands;

namespace Factum.Modules.Users.Core.Commands;

internal record SignIn([Required][EmailAddress] string Email, [Required] string Password) : ICommand
{
    public Guid Id { get; init; } = Guid.NewGuid();
}