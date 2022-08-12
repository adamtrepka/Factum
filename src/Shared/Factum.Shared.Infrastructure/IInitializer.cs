using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure;

public interface IInitializer
{
    Task InitAsync();
}