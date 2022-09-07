using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Types;

namespace Factum.Modules.Documents.Application.Documents.Policies
{
    internal interface IDocumentRevokeAccessPolicy
    {
        bool CanRevoke(Document document, UserId revokeTo);
    }
}
