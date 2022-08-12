using System;

namespace Factum.Shared.Abstractions.Exceptions;

public abstract class ModularException : Exception
{
    protected ModularException(string message) : base(message)
    {
    }
}