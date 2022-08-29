using Chronicle;
using Factum.Modules.Saga.Core.Messages.External;
using Factum.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Core.Handlers
{
    internal class SagaEventHandler : IEventHandler<BlockAdded>, IEventHandler<BlockValidated>, IEventHandler<BlockRejected>
    {
        private readonly ISagaCoordinator _sagaCoordinator;

        public SagaEventHandler(ISagaCoordinator sagaCoordinator)
        {
            _sagaCoordinator = sagaCoordinator ?? throw new ArgumentNullException(nameof(sagaCoordinator));
        }

        public Task HandleAsync(BlockAdded @event, CancellationToken cancellationToken = default)
            => HandleAsync(@event);

        public Task HandleAsync(BlockRejected @event, CancellationToken cancellationToken = default)
            => HandleAsync(@event);

        public Task HandleAsync(BlockValidated @event, CancellationToken cancellationToken = default)
            => HandleAsync(@event);

        private Task HandleAsync<T>(T message) where T : class
            => _sagaCoordinator.ProcessAsync(message, SagaContext.Empty);
    }
}
