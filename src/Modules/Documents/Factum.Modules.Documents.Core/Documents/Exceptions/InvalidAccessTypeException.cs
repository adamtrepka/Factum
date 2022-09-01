using Factum.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Core.Documents.Exceptions
{
    internal class InvalidAccessTypeException : FactumException
    {
        public InvalidAccessTypeException() : base($"Access type cannot be null or empty")
        {
        }

    }
}
