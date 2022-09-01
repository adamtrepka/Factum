using Factum.Shared.Abstractions.Kernel.Types;
using System;

namespace Factum.Modules.Documents.Core.Documents.Types
{
    internal class AccessId : TypeId
    {
        public AccessId() : base(Guid.NewGuid())
        {

        }
        public AccessId(Guid value) : base(value)
        {
        }
        public override string ToString() => Value.ToString();
        public static implicit operator AccessId(Guid id) => new(id);
    }
}
