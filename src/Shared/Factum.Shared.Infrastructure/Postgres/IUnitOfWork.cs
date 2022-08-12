using System;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.Postgres;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}