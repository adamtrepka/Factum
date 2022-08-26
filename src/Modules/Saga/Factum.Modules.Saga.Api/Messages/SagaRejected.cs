using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Saga.Api.Messages
{
    internal record SagaRejected() : IEvent;
}
