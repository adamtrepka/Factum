using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Documents.Core.Documents.Exceptions
{
    internal class AccessNotFoundException : FactumException
    {
        public AccessNotFoundException(DocumentId documentId, UserId userId) : base($"User '{userId}' does not have access to document '{documentId}'")
        {
            DocumentId = documentId;
            UserId = userId;
        }

        public DocumentId DocumentId { get; }
        public AccessType AccessType { get; }
        public UserId UserId { get; }
    }
}
