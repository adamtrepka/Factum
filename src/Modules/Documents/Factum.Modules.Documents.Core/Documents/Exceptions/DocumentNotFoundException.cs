using Factum.Shared.Abstractions.Exceptions;
using System;

namespace Factum.Modules.Documents.Core.Documents.Exceptions;

public class DocumentNotFoundException : FactumException
{
    public DocumentNotFoundException(Guid documentId) : base($"Document with ID: '{documentId}' was not found.")
    {
        DocumentId = documentId;
    }

    public Guid DocumentId { get; }
}


