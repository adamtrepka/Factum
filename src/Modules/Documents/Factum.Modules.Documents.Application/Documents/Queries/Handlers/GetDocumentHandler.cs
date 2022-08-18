using Factum.Modules.Documents.Core.Documents.DTO;
using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Queries.Handlers
{
    internal class GetDocumentHandler : IQueryHandler<GetDocument, DocumentDto>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetDocumentHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }
        public async Task<DocumentDto> HandleAsync(GetDocument query, CancellationToken cancellationToken = default)
        {
            var document = await _documentRepository.GetAsync(query.DocumentId);

            if (document is not null)
            {
                var dto = new DocumentDto
                {
                    DocumentId = document.BusinessId,
                    FileName = document.FileName,
                    ContentType = document.ContentType
                };
                return dto;
            }

            return default;
        }
    }
}
