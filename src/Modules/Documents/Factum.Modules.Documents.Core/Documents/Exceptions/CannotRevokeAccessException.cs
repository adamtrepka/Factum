using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Documents.Core.Documents.Exceptions
{
    internal class CannotRevokeAccessException : FactumException
    {
        public CannotRevokeAccessException(UserId revokedBy, UserId revokedTo, DocumentId documentId) : base($"User '{revokedBy}' cannot revoke access user '{revokedTo}' to document '{documentId}'")
        {
            RevokedBy = revokedBy;
            RevokedTo = revokedTo;
            DocumentId = documentId;
        }

        public UserId RevokedBy { get; }
        public UserId RevokedTo { get; }
        public DocumentId DocumentId { get; }
    }
}
