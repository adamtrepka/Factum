using Factum.Shared.Abstractions.Kernel.Types;

namespace Factum.Modules.Ledger.Core.Domain.Types
{
    internal class BlockId : TypeId
    {
        public BlockId() : base(Guid.NewGuid())
        {

        }

        public BlockId(Guid value) : base(value)
        {
        }
        public override string ToString() => Value.ToString();
    }
}
