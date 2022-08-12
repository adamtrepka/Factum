using Factum.Shared.Abstractions.Contexts;
using System;

namespace Factum.Shared.Abstractions.Messaging;

public interface IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }
}