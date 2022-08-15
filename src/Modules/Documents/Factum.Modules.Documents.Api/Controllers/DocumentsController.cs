using Factum.Shared.Abstractions.Contexts;
using Factum.Shared.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : Controller
    {
        private readonly IDispatcher _dispatcher;
        private readonly IContext _context;

        public DocumentsController(IDispatcher dispatcher, IContext context)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
