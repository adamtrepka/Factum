using Factum.Shared.Abstractions.Messaging;

namespace Factum.Shared.Infrastructure.Messaging.Dispatchers;

public record MessageEnvelope(IMessage Message, IMessageContext MessageContext);