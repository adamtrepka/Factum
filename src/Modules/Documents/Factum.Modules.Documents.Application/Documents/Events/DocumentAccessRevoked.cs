using Factum.Shared.Abstractions.Events;
using System;

namespace Factum.Modules.Documents.Application.Documents.Events
{
    internal record DocumentAccessRevoked(Guid AccessId, Guid DocumentId, string AccessType, Guid RevokedBy, Guid RevokedTo) : IEvent;
}
