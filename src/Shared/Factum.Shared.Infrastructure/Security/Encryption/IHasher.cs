using System.IO;

namespace Factum.Shared.Infrastructure.Security.Encryption;

public interface IHasher
{
    string Hash(string data);
    byte[] Hash(byte[] bytes);
    byte[] Hash(Stream stream);
}