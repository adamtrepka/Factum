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

        private readonly List<Access> _accesses = new();

        public IReadOnlyList<Access> Accesses => _accesses.ToList().AsReadOnly();

        private Document()
        {

        }

        public Document(DocumentId documentId, DateTime createdAt)
        {
            BusinessId = documentId;
            CreatedAt = createdAt;

            AddEvent(new DocumentCreated(this));
        }

        public void AttachedFile(string name, string contentType, byte[] hash)
        {
            File = new File(name, contentType, hash);
        }

        public void GrantAccess(AccessType type, UserId grantedBy, UserId grantedTo)
        {
            if (_accesses.Any(x => x.GrantedTo == grantedBy)) throw new AccessAlreadyGrantedException(BusinessId, type, grantedTo);

            var access = new Access(BusinessId, type, grantedBy, grantedTo);
            _accesses.Add(access);

            AddEvent(new AccessGranted(access));
            IncrementVersion();
        }

        public void RevokeAccess(UserId userId, UserId revokedBy)
        {
            var access = _accesses.FirstOrDefault(x => x.GrantedTo == userId);

            if (access is null) throw new AccessNotFoundException(BusinessId, userId);

            _accesses.Remove(access);

            AddEvent(new AccessRevoked(access, revokedBy));
            IncrementVersion();
        }
    }
}
