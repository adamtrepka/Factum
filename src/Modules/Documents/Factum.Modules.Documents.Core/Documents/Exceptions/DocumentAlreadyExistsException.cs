using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Documents.Core.Documents.Exceptions;

public class DocumentAlreadyExistsException : FactumException
{
    public DocumentAlreadyExistsException(Guid documentId) : base($"Document for owner with ID: '{documentId}' already exists.")
    {
        DocumentId = documentId;
    }

    public Guid DocumentId { get; }
}


