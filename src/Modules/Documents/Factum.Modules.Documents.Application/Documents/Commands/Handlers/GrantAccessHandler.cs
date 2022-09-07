using Factum.Modules.Documents.Application.Documents.Policies;
using Factum.Modules.Documents.Core.Documents.Exceptions;
using Factum.Modules.Documents.Core.Documents.Repositories;
using Factum.Shared.Abstractions.Commands;
using Factum.Shared.Abstractions.Contexts;
using Factum.Shared.Abstractions.Kernel;
using Factum.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Commands.Handlers
{
    internal class GrantAccessHandler : ICommandHandler<GrantAccess>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentAccessPolicy _documentAccessPolicy;
        private readonly IDocumentGrantAccessPolicy _documentGrantAccessPolicy;
        private readonly IContext _context;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public GrantAccessHandler(IDocumentRepository documentRepository,
                                  IDocumentAccessPolicy documentAccessPolicy,
                                  IDocumentGrantAccessPolicy documentGrantAccessPolicy,
                                  IContext context,
                                  IEventMapper eventMapper,
                                  IMessageBroker messageBroker)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _documentAccessPolicy = documentAccessPolicy ?? throw new ArgumentNullException(nameof(documentAccessPolicy));
            _documentGrantAccessPolicy = documentGrantAccessPolicy ?? throw new ArgumentNullException(nameof(documentGrantAccessPolicy));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventMapper = eventMapper ?? throw new ArgumentNullException(nameof(eventMapper));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
        }
        public async Task HandleAsync(GrantAccess command, CancellationToken cancellationToken = default)
        {
            var document = await _documentRepository.GetAsync(command.DocumentId, access => access.GrantedTo == command.GrantAccessTo || access.GrantedTo == _context.Identity.Id);

            if (_documentAccessPolicy.IsDocumentAccessAllowed(document) is false)
            {
                throw new AccessNotFoundException(document.BusinessId, _context.Identity.Id);
            }

            if (_documentGrantAccessPolicy.CanGrant(document) is false)
            {
                throw new CannotGrantAccessException(_context.Identity.Id, command.GrantAccessTo, command.DocumentId);
            }

            document.GrantAccess(command.AccessType, _context.Identity.Id, command.GrantAccessTo);

            var messages = _eventMapper.MapAll(document.Events);

            await _documentRepository.UpdateAsync(document);
            await _messageBroker.PublishAsync(messages, cancellationToken);
        }
    }
}
