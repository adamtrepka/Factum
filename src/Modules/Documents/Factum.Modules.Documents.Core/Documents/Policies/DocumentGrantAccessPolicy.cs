using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Exceptions;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Factum.Shared.Abstractions.Contexts;
using System.Linq;

namespace Factum.Modules.Documents.Application.Documents.Policies
{
    internal class DocumentGrantAccessPolicy : IDocumentGrantAccessPolicy
    {
        private readonly IContext _context;

        public DocumentGrantAccessPolicy(IContext context)
        {
            _context = context;
        }
        public bool CanGrant(Document document)
        {
            var userAccess = document.Accesses.FirstOrDefault(x => x.GrantedTo == _context.Identity.Id);

            if (userAccess is null) return false;
            if (userAccess.AccessType == new AccessType("reader")) return false;
            return true;
        }
    }
}
