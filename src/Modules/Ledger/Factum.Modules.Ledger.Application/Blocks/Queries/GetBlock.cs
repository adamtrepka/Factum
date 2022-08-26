using Factum.Modules.Ledger.Application.Blocks.DTO;
using Factum.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Application.Blocks.Queries
{
    internal class GetBlock : IQuery<BlockDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
