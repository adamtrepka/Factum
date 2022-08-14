using System;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.SqlServer;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}