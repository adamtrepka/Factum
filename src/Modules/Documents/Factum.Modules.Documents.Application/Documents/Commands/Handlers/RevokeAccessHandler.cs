using Factum.Modules.Documents.Application.Documents.Policies;
using Factum.Modules.Documents.Core.Documents.Exceptions;
using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Shared.Abstractions.Commands;
using Factum.Shared.Abstractions.Contexts;
using Factum.Shared.Abstractions.Kernel;
using Factum.Shared.Abstractions.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Commands.Handlers
{
    internal class RevokeAccessHandler : ICommandHandler<RevokeAccess>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentAccessPolicy _documentAccessPolicy;
        private readonly IDocumentRevokeAccessPolicy _documentRevokeAccessPolicy;
        private readonly IContext _context;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public RevokeAccessHandler(IDocumentRepository documentRepository,
                                   IDocumentAccessPolicy documentAccessPolicy,
                                   IDocumentRevokeAccessPolicy documentRevokeAccessPolicy,
                                   IContext context,
                                   IEventMapper eventMapper,
                                   IMessageBroker messageBroker)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _documentAccessPolicy = documentAccessPolicy ?? throw new ArgumentNullException(nameof(documentAccessPolicy));
            _documentRevokeAccessPolicy = documentRevokeAccessPolicy ?? throw new ArgumentNullException(nameof(documentRevokeAccessPolicy));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventMapper = eventMapper ?? throw new ArgumentNullException(nameof(eventMapper));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
        }
        public async Task HandleAsync(RevokeAccess command, CancellationToken cancellationToken = default)
        {
            var document = await _documentRepository.GetAsync(command.DocumentId, access => access.GrantedTo == command.RevokeAccessTo || access.GrantedTo == _context.Identity.Id);

            if (_documentAccessPolicy.IsDocumentAccessAllowed(document) is false)
            {
                throw new AccessNotFoundException(document.BusinessId, _context.Identity.Id);
            }

            if (_documentRevokeAccessPolicy.CanRevoke(document, command.RevokeAccessTo) is false)
            {
                throw new CannotRevokeAccessException(_context.Identity.Id, command.RevokeAccessTo, command.DocumentId);
            }

            document.RevokeAccess(command.RevokeAccessTo, _context.Identity.Id);

            var messages = _eventMapper.MapAll(document.Events);

            await _documentRepository.UpdateAsync(document);
            await _messageBroker.PublishAsync(messages, cancellationToken);
        }
    }
}
