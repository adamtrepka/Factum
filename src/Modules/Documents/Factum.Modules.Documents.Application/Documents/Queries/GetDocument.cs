using Factum.Modules.Documents.Core.Documents.DTO;
using Factum.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Queries
{
    internal class GetDocument : IQuery<DocumentDto>
    {
        public Guid DocumentId { get; set; }
    }
}
