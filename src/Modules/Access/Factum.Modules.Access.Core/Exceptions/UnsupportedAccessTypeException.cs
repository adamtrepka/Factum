using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Access.Core.Exceptions
{
    internal class UnsupportedAccessTypeException : FactumException
    {
        public UnsupportedAccessTypeException(string accessType) : base($"Access type: '{accessType}' is unsupported.")
        {
            AccessType = accessType;
        }

        public string AccessType { get; }
    }
}
