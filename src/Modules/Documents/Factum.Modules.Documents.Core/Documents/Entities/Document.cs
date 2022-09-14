using Factum.Modules.Documents.Core.Documents.Events;
using Factum.Modules.Documents.Core.Documents.Exceptions;
using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Factum.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factum.Modules.Documents.Core.Documents.Entities
{
    internal class Document : AggregateRoot<DocumentId>
    {
        public File File { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<Entitlement> _entitlements = new();

        public IReadOnlyList<Entitlement> Entitlements => _entitlements.ToList().AsReadOnly();

        private Document()
        {

        }

        public Document(DocumentId documentId, UserId createdBy, DateTime createdAt)
        {
            BusinessId = documentId;
            CreatedAt = createdAt;

            AddEvent(new DocumentCreated(this, createdBy));
        }

        public void AttachedFile(string name, string contentType, byte[] hash)
        {
            File = new File(name, contentType, hash);
        }

        public void AddEntitlement(UserId userId)
        {
            if (_entitlements.Any(x => x.UserId == userId)) return;

            var entitlement = new Entitlement(this.BusinessId, userId);
            _entitlements.Add(entitlement);

            IncrementVersion();
        }

        public void RemoveEntitlement(UserId userId)
        {
            var entitlement = _entitlements.FirstOrDefault(x => x.UserId == userId);

            if (entitlement is not null)
            {
                _entitlements.Remove(entitlement);

                IncrementVersion();
            }
        }
    }
}
