using System;

namespace Factum.Shared.Infrastructure.Serialization;

public interface IJsonSerializer
{
    string SerializeToJsonString<T>(T value);
    T Deserialize<T>(string value);
    object Deserialize(string value, Type type);
    byte[] SerializeToUtf8Bytes<T>(T value);
    T Deserialize<T>(byte[] value);
}