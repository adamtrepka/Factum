using Factum.Modules.Ledger.Application.Blocks.Commands;
using Factum.Modules.Ledger.Core.Entries.Events;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Abstractions.Events;
using Factum.Shared.Abstractions.Kernel;

namespace Factum.Modules.Ledger.Application.Blocks.Events.Externals.Handlers
{
    internal class SagaHandler : IEventHandler<SagaComplated>, IEventHandler<SagaRejected>
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public SagaHandler(IDomainEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }
        public async Task HandleAsync(SagaComplated @event, CancellationToken cancellationToken = default)
        {
            await _dispatcher.DispatchAsync(new EntryAdded(), cancellationToken);
        }

        public async Task HandleAsync(SagaRejected @event, CancellationToken cancellationToken = default)
        {
            await _dispatcher.DispatchAsync(new EntryAdded(), cancellationToken);
        }
    }
}
