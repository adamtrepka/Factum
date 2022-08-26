using Factum.Shared.Abstractions.Kernel.Types;

namespace Factum.Modules.Ledger.Core.Blocks.Types
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
        public static implicit operator BlockId(Guid value) => new(value);

    }
}
