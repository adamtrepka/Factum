using Factum.Modules.Documents.Application.Documents.Commands;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Factum.Shared.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(DocumentsModule.DocumentsPolicyName)]
internal class AccessController : Controller
{
    private readonly IDispatcher _dispatcher;

    public AccessController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
    }

    [HttpPost("")]
    [SwaggerOperation("Grant access to document")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GrantAccess(Guid documentId, Guid userId, string accessType)
    {
        await _dispatcher.SendAsync(new GrantAccess(documentId,accessType,userId));
        return NoContent();
    }

    [HttpDelete("")]
    [SwaggerOperation("Revoke access to document")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> RevokeAccess(Guid documentId, Guid userId)
    {
        await _dispatcher.SendAsync(new RevokeAccess(documentId, userId));
        return NoContent();
    }
}
