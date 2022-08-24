using Factum.Modules.Ledger.Core.Clients.Documents.Dto;
using Factum.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Core.Clients.Documents
{
    internal class DocumentApiClient : IDocumentApiClient
    {
        private readonly IModuleClient _moduleClient;

        public DocumentApiClient(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient ?? throw new ArgumentNullException(nameof(moduleClient));
        }

        public Task<DocumentDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
            => _moduleClient.SendAsync<DocumentDto>("documents/get", new { DocumentId = id }, cancellationToken);
    }
}
