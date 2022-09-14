using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Shared.Abstractions.Contexts;
using System.Linq;

namespace Factum.Modules.Documents.Application.Documents.Policies
{
    internal class DocumentAccessPolicy : IDocumentAccessPolicy
    {
        private readonly IContext _context;

        public DocumentAccessPolicy(IContext context)
        {
            _context = context;
        }
        public bool IsDocumentAccessAllowed(Document document)
        {
            if (document?.Entitlements?.Any(x => x.UserId == _context.Identity.Id) ?? false)
                return true;
            else
                return false; 
        }
    }
}
