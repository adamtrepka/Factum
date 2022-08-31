using Factum.Modules.Ledger.Application.Blocks.DTO;
using Factum.Modules.Ledger.Application.Blocks.Queries;
using Factum.Shared.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ProofsController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public ProofsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        [HttpGet("Documents/{documentId:guid}")]
        [SwaggerOperation("Get proof for document")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ProofDto>> GetDocumentProofAsync(Guid documentId)
        {
            var result = await _dispatcher.QueryAsync(new GetProof(documentId));
            return result is not null ? Ok(result) : NotFound();
        }
    }
}
