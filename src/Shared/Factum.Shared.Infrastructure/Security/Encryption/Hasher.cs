using Factum.Shared.Infrastructure.Serialization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Factum.Shared.Infrastructure.Security.Encryption;

public sealed class Hasher : IHasher
{
    private readonly IJsonSerializer _jsonSerializer;

    public Hasher(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer ?? throw new System.ArgumentNullException(nameof(jsonSerializer));
    }
    public string Hash(string data)
    {
        using var sha512 = SHA512.Create();
        var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(data));
        var builder = new StringBuilder();
        foreach (var @byte in bytes)
        {
            builder.Append(@byte.ToString("x2"));
        }

        return builder.ToString();
    }
    public byte[] Hash(byte[] bytes)
    {
        using var sha512 = SHA512.Create();
        var hash = sha512.ComputeHash(bytes);
        return hash;
    }

    public byte[] Hash(Stream stream)
    {
        using var sha512 = SHA512.Create();
        var hash = sha512.ComputeHash(stream);
        return hash;
    }

    public byte[] Hash<T>(T value)
    {
        var data = _jsonSerializer.SerializeToUtf8Bytes(value);
        using var sha512 = SHA512.Create();
        return sha512.ComputeHash(data);
    }

    public bool Validate<T>(T value, byte[] hash)
    {
        var valueHash = Hash(value);
        return hash.SequenceEqual(valueHash);
    }
}