using Factum.Modules.Documents.Application.Documents.Events;
using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Shared.Abstractions.Commands;
using Factum.Shared.Abstractions.Messaging;
using Factum.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Commands.Handlers
{
    internal class AddDocumentHandler : ICommandHandler<AddDocument>
    {
        private readonly IClock _clock;
        private readonly IDocumentRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<AddDocumentHandler> _logger;

        public AddDocumentHandler(IClock clock, IDocumentRepository repository, IMessageBroker messageBroker, ILogger<AddDocumentHandler> logger)
        {
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task HandleAsync(AddDocument command, CancellationToken cancellationToken = default)
        {
            using var ms = new MemoryStream();

            command.file.CopyTo(ms);
            var fileBytes = ms.ToArray();

            var document = new Document(command.documentId, command.file.FileName, fileBytes, _clock.CurrentDate());
            await _repository.AddAsync(document);
            
            await _messageBroker.PublishAsync(new DocumentAdded(document.BusinessId), cancellationToken);

            _logger.LogInformation($"Created a new document with ID: '{document.BusinessId}'");

        }
    }
}
