using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Shared.Abstractions.Kernel.Types;

namespace Factum.Modules.Documents.Core.Documents.Entities
{
    internal class Document : AggregateRoot<DocumentId>
    {
        public string FileName { get; private set; }
        public byte[] File { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Document()
        {

        }

        public Document(DocumentId documentId, string fileName, byte[] file, DateTime createdAt)
        {
            BusinessId = documentId;
            FileName = fileName;
            File = file;
            CreatedAt = createdAt;
        }
    }
}
