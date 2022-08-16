using System;

namespace Factum.Shared.Abstractions.Exceptions;

public abstract class FactumException : Exception
{
    protected FactumException(string message) : base(message)
    {
    }
}