using Factum.Shared.Abstractions.Exceptions;

namespace Factum.Modules.Documents.Core.Documents.Exceptions
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
