using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Factum.Shared.Infrastructure.Serialization;

public class SystemTextJsonSerializer : IJsonSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() },
        ReferenceHandler = ReferenceHandler.IgnoreCycles        
    };

    public string SerializeToJsonString<T>(T value) => JsonSerializer.Serialize(value, Options);

    public byte[] SerializeToUtf8Bytes<T>(T value) => JsonSerializer.SerializeToUtf8Bytes(value, Options);

    public T Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value, Options);
    public T Deserialize<T>(byte[] value) => JsonSerializer.Deserialize<T>(value,Options);

    public object Deserialize(string value, Type type) => JsonSerializer.Deserialize(value, type, Options);

}