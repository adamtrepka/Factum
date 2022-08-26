using Factum.Modules.Ledger.Application.Blocks.Commands;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Ledger.Application.Blocks.Events.Externals.Handlers
{
    internal class SagaComplatedHandler : IEventHandler<SagaComplated>
    {
        private readonly IDispatcher _dispatcher;

        public SagaComplatedHandler(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }
        public async Task HandleAsync(SagaComplated @event, CancellationToken cancellationToken = default)
        {
            await _dispatcher.SendAsync(new CreateNewBlock(), cancellationToken);
        }
    }
}
