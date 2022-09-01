using Factum.Shared.Abstractions.Events;
using System;

namespace Factum.Modules.Documents.Application.Documents.Events
{
    internal record DocumentAccessGranted(Guid AccessId, Guid DocumentId, string AccessType, Guid GrantedBy, Guid GrantedTo) : IEvent;
}
