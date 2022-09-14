using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Access.Core.Events
{
    internal record AccessRevoked(Guid DocumentId, Guid RevokedBy, Guid RevokedTo) : IEvent;
}
