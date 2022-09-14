using Factum.Shared.Abstractions.Kernel.Types;
using System;

namespace Factum.Modules.Documents.Core.Documents.Types
{
    internal class EntitlementId : TypeId
    {
        public EntitlementId() : base(Guid.NewGuid())
        {

        }
        public EntitlementId(Guid value) : base(value)
        {
        }
        public override string ToString() => Value.ToString();
        public static implicit operator EntitlementId(Guid id) => new(id);
    }
}
