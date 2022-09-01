﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Factum.Shared.Abstractions.Commands;
using Factum.Modules.Users.Core.Commands;

namespace Factum.Modules.Users.Core.Commands.Handlers;

internal sealed class SignOutHandler : ICommandHandler<SignOut>
{
    private readonly ILogger<SignOutHandler> _logger;

    public SignOutHandler(ILogger<SignOutHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(SignOut command, CancellationToken cancellationToken = default)
    {

        await Task.CompletedTask;
        _logger.LogInformation($"User with ID: '{command.UserId}' has signed out.");
    }
}