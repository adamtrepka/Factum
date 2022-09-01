using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Documents.Core.Documents.Exceptions
{
    internal class AccessAlreadyGrantedException : FactumException
    {
        public AccessAlreadyGrantedException(DocumentId documentId, AccessType accessType, UserId userId) : base($"User '{userId}' already has access '{accessType}' to document '{documentId}'")
        {
            DocumentId = documentId;
            AccessType = accessType;
            UserId = userId;
        }

        public DocumentId DocumentId { get; }
        public AccessType AccessType { get; }
        public UserId UserId { get; }
    }
}
