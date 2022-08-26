using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Validation.Core.Events
{
    internal record BlockRejected(Guid BlockId) : IEvent;

}
