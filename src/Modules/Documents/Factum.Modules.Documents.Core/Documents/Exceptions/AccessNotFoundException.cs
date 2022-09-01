﻿using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Documents.Core.Documents.Exceptions
{
    internal class AccessNotFoundException : FactumException
    {
        public AccessNotFoundException(DocumentId documentId, AccessType accessType, UserId userId) : base($"User '{userId}' does not have access '{accessType}' to document '{documentId}'")
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
