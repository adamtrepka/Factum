using Factum.Modules.Documents.Core.Documents.Entities;

namespace Factum.Modules.Documents.Application.Documents.Policies
{
    internal interface IDocumentGrantAccessPolicy
    {
        bool CanGrant(Document document);
    }
}
