using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Shared.Abstractions.Kernel.Types;
using System;

namespace Factum.Modules.Documents.Core.Documents.Entities
{
    internal class Document : AggregateRoot<DocumentId>
    {
        public string FileName { get; private set; }
        public string ContentType { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Document()
        {

        }

        public Document(DocumentId documentId, string fileName, string contentType, DateTime createdAt)
        {
            BusinessId = documentId;
            FileName = fileName;
            ContentType = contentType;
            CreatedAt = createdAt;
        }
    }
}
