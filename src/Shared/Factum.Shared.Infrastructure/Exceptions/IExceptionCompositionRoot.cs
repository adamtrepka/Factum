using Factum.Shared.Abstractions.Exceptions;
using System;

namespace Factum.Shared.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}