using Factum.Modules.Documents.Application.Documents.Events;
using Factum.Modules.Documents.Core.Documents.Entities;
using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Shared.Abstractions.Blob;
using Factum.Shared.Abstractions.Commands;
using Factum.Shared.Abstractions.Contexts;
using Factum.Shared.Abstractions.Kernel;
using Factum.Shared.Abstractions.Messaging;
using Factum.Shared.Abstractions.Time;
using Factum.Shared.Infrastructure.Security.Encryption;
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
        private readonly IBlobStorage _blobStorage;
        private readonly IHasher _hasher;
        private readonly IContext _context;
        private readonly IEventMapper _eventMapper;

        public AddDocumentHandler(IClock clock,
                                  IDocumentRepository repository,
                                  IMessageBroker messageBroker,
                                  ILogger<AddDocumentHandler> logger,
                                  IBlobStorage blobStorage,
                                  IHasher hasher,
                                  IContext context,
                                  IEventMapper eventMapper)
        {
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _blobStorage = blobStorage ?? throw new ArgumentNullException(nameof(blobStorage));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventMapper = eventMapper ?? throw new ArgumentNullException(nameof(eventMapper));
        }
        public async Task HandleAsync(AddDocument command, CancellationToken cancellationToken = default)
        {
            var document = new Document(command.documentId, _clock.CurrentDate());

            using var fileStream = command.file.OpenReadStream();

            var fileHash = _hasher.Hash(fileStream);
            document.AttachedFile(command.file.Name, command.file.ContentType, fileHash);

            document.GrantAccess("owner", _context.Identity.Id, _context.Identity.Id);

            await _repository.AddAsync(document);

            await _blobStorage.UploadAsync(fileStream, document.BusinessId.ToString(), command.file.FileName, cancellationToken);

            var messages = _eventMapper.MapAll(document.Events);
            
            await _messageBroker.PublishAsync(messages, cancellationToken);

            _logger.LogInformation($"Created a new document with ID: '{document.BusinessId}'");

        }
    }
}
