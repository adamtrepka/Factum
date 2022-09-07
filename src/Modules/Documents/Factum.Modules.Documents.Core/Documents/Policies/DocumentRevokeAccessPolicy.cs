using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Factum.Shared.Abstractions.Contexts;
using System.Linq;

namespace Factum.Modules.Documents.Application.Documents.Policies
{
    internal class DocumentRevokeAccessPolicy : IDocumentRevokeAccessPolicy
    {
        private readonly IContext _context;

        public DocumentRevokeAccessPolicy(IContext context)
        {
            _context = context;
        }

        public bool CanRevoke(Document document, UserId revokeTo)
        {
            var userAccess = document.Accesses.FirstOrDefault(x => x.GrantedTo == _context.Identity.Id);
            var accesToRevoke = document.Accesses.FirstOrDefault(x => x.GrantedTo == revokeTo);

            if (userAccess is null || accesToRevoke is null) return false;
            if (userAccess.AccessType == new AccessType("reader")) return false;
            if (userAccess.AccessType == new AccessType("owner")) return true;
            if (userAccess.AccessType == new AccessType("contributor") && accesToRevoke.GrantedBy == _context.Identity.Id) return true;

            return false;
        }
    }
}
