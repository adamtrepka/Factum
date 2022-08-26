using Factum.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Entries.Types
{
    internal class EntryId : TypeId
    {
        public EntryId() : base(Guid.NewGuid())
        {

        }
        public EntryId(Guid value) : base(value)
        {
        }
        public override string ToString() => Value.ToString();
    }
}
