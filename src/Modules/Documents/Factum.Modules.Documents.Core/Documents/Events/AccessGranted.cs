using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Shared.Abstractions.Kernel;

namespace Factum.Modules.Documents.Core.Documents.Events
{
    internal class AccessGranted : IDomainEvent
    {
        public AccessGranted(Access access)
        {
            Access = access;
        }

        public Access Access { get; }
    }
}
