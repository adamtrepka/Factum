using Factum.Modules.Documents.Core.Documents.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Policies
{
    internal interface IDocumentAccessPolicy
    {
        bool IsDocumentAccessAllowed(Document document);
    }
}
