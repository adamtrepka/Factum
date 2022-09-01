using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Shared.Abstractions.Kernel;

namespace Factum.Modules.Documents.Core.Documents.Events
{
    internal class AccessRevoked : IDomainEvent
    {
        public AccessRevoked(Access access, UserId revokedBy)
        {
            Access = access;
            RevokedBy = revokedBy;
        }

        public Access Access { get; }
        public UserId RevokedBy { get; }
    }
}
