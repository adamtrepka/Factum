using Factum.Shared.Abstractions.Contexts;
using Factum.Shared.Abstractions.Messaging;
using System;

namespace Factum.Shared.Infrastructure.Messaging.Contexts;

public class MessageContext : IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }

    public MessageContext(Guid messageId, IContext context)
    {
        MessageId = messageId;
        Context = context;
    }
}