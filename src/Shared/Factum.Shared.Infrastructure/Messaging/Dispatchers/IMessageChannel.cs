﻿using System.Threading.Channels;

namespace Factum.Shared.Infrastructure.Messaging.Dispatchers;

public interface IMessageChannel
{
    ChannelReader<MessageEnvelope> Reader { get; }
    ChannelWriter<MessageEnvelope> Writer { get; }
}