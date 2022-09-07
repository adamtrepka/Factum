using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Shared.Abstractions.Commands;
using System;

namespace Factum.Modules.Documents.Application.Documents.Commands;

internal record RevokeAccess(Guid DocumentId, UserId RevokeAccessTo) : ICommand;

