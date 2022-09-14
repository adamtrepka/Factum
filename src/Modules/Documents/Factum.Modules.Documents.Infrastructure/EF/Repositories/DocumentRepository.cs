using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Infrastructure.EF.Repositories
{
    internal class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentsDbContext _context;
        private readonly DbSet<Document> _documents;

        public DocumentRepository(DocumentsDbContext documentsDbContext)
        {
            _context = documentsDbContext ?? throw new ArgumentNullException(nameof(documentsDbContext));
            _documents = _context.Documents;
        }
        public async Task AddAsync(Document wallet)
        {
            await _documents.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public Task<Document> GetAsync(DocumentId id)
            => _documents.Include(x => x.Entitlements)
                         .SingleOrDefaultAsync(x => x.BusinessId == id);


        public async Task UpdateAsync(Document document)
        {
            _documents.Update(document);
            await _context.SaveChangesAsync();
        }
    }
}
