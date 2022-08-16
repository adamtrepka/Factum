using Factum.Shared.Infrastructure.SqlServer;

namespace Factum.Modules.Documents.Infrastructure.EF;

internal class DocumentsUnitOfWork : SqlServerUnitOfWork<DocumentsDbContext>
{
    public DocumentsUnitOfWork(DocumentsDbContext dbContext) : base(dbContext)
    {
    }
}

