using Factum.Modules.Documents.Application.Documents.Commands;
using Factum.Modules.Documents.Application.Documents.Queries;
using Factum.Modules.Documents.Core.Documents.DTO;
using Factum.Shared.Abstractions.Contexts;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Api.Controllers;

[ApiController]
[Route("[controller]")]
internal class DocumentsController : Controller
{
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public DocumentsController(IDispatcher dispatcher, IContext context)
    {
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("{documentId:guid}")]
    [SwaggerOperation("Get document")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<DocumentDto>> GetAsync(Guid documentId)
    {
        var result = await _dispatcher.QueryAsync(new GetDocument { DocumentId = documentId });

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [SwaggerOperation("Add document")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Post([FromForm] AddDocument command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }
}

