using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Factum.Shared.Abstractions.Kernel.Types;
using System;

namespace Factum.Modules.Documents.Core.Documents.Entities
{
    internal class Document : AggregateRoot<DocumentId>
    {
        public File File { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Document()
        {

        }

        public Document(DocumentId documentId, DateTime createdAt)
        {
            BusinessId = documentId;
            CreatedAt = createdAt;
        }

        public void AttachedFile(string name, string contentType, byte[] hash)
        {
            File = new File(name, contentType, hash);
        }
    }
}
