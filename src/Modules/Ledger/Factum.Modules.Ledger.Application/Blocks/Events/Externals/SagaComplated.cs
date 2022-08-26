using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;

namespace Factum.Modules.Ledger.Application.Blocks.Events.Externals
{
    internal record SagaComplated() : IEvent;

    [Message("Validation")]
    internal class SagaComplatedContract : Contract<SagaComplated>
    {
        public SagaComplatedContract()
        {
        }
    }
}
