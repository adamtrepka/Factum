using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;

namespace Factum.Modules.Documents.Core.Documents.Entities
{
    internal class Access
    {
        public int Id { get; set; }
        public AccessId BusinessId { get; set; }
        public DocumentId DocumentId { get; set; }
        public Document Document { get; set; }
        public AccessType AccessType { get; set; }

        public UserId GrantedBy { get; set; }
        public UserId GrantedTo { get; set; }

        private Access()
        {

        }

        public Access(DocumentId documentId, AccessType accessType, UserId grantedBy, UserId grantedTo)
        {
            BusinessId = new();
            DocumentId = documentId;
            AccessType = accessType;
            GrantedBy = grantedBy;
            GrantedTo = grantedTo;
        }
    }
}
