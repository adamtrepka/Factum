using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Messaging;

namespace Factum.Modules.Ledger.Application.Blocks.Events.Externals
{
    internal record SagaRejected() : IEvent;

    [Message("Saga")]
    internal class SagaRejectedContract : Contract<SagaRejected>
    {
        public SagaRejectedContract()
        {
        }
    }
}
