using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Core.Documents.ValueObjects
{
    internal record File(string Name, string ContentType, byte[] Hash);
}
