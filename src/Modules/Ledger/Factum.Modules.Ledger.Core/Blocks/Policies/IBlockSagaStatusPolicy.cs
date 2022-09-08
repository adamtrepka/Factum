using Factum.Modules.Ledger.Core.Blocks.Entities;

namespace Factum.Modules.Ledger.Core.Blocks.Policies
{
    internal interface IBlockSagaStatusPolicy
    {
        Task<bool> IsPending(Block block);
    }
}
