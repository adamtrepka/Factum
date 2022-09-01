using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Shared.Abstractions.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Core.Documents.Events
{
    internal class DocumentCreated : IDomainEvent
    {
        public DocumentCreated(Document document)
        {
            Document = document;
        }

        public Document Document { get; }
    }
}
