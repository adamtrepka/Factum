using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Factum.Shared.Infrastructure.Security.Encryption;

public sealed class Hasher : IHasher
{
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
}