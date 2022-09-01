using Factum.Shared.Abstractions.Kernel.Types;
using System;

namespace Factum.Modules.Documents.Core.Documents.Types
{
    internal class UserId : TypeId
    {
        public UserId(Guid value) : base(value)
        {
        }
        public static implicit operator UserId(Guid id) => new(id);

        public override string ToString() => Value.ToString();
    }
}
