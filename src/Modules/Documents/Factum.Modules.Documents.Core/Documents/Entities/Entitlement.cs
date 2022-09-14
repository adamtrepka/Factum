using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;

namespace Factum.Modules.Documents.Core.Documents.Entities
{
    internal class Entitlement
    {
        public int Id { get; set; }
        public DocumentId DocumentId { get; set; }
        public UserId UserId { get; set; }

        private Entitlement()
        {

        }

        public Entitlement(DocumentId documentId, UserId userId)
        {
            DocumentId = documentId;
            UserId = userId;
        }
    }
}
