using Factum.Shared.Abstractions.Kernel.Types;

namespace Factum.Modules.Access.Core.Types
{
    internal class DocumentId : TypeId
    {
        public DocumentId() : base(Guid.NewGuid())
        {

        }
        public DocumentId(Guid value) : base(value)
        {
        }

        public static implicit operator DocumentId(Guid id) => new(id);

        public override string ToString() => Value.ToString();
    }
}
