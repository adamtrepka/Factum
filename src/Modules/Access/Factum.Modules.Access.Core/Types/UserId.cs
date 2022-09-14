using Factum.Shared.Abstractions.Kernel.Types;

namespace Factum.Modules.Access.Core.Types
{
    internal class UserId : TypeId
    {
        public UserId() : base(Guid.NewGuid())
        {

        }
        public UserId(Guid value) : base(value)
        {
        }

        public static implicit operator UserId(Guid id) => new(id);

        public override string ToString() => Value.ToString();
    }
}
