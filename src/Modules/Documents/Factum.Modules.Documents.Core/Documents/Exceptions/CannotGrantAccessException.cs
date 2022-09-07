using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Documents.Core.Documents.Exceptions
{
    internal class CannotGrantAccessException : FactumException
    {
        public CannotGrantAccessException(UserId grantedBy, UserId grantedTo, DocumentId documentId) : base($"User '{grantedBy}' cannot grant access user '{grantedTo}' to document '{documentId}'")
        {
            GrantedBy = grantedBy;
            GrantedTo = grantedTo;
            DocumentId = documentId;
        }

        public UserId GrantedBy { get; }
        public UserId GrantedTo { get; }
        public DocumentId DocumentId { get; }
    }
}
