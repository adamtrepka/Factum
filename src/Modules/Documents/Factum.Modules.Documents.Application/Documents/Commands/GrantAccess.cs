using Factum.Modules.Documents.Core.Documents.Types;
using Factum.Modules.Documents.Core.Documents.ValueObjects;
using Factum.Shared.Abstractions.Commands;
using System;

namespace Factum.Modules.Documents.Application.Documents.Commands;

internal record GrantAccess(Guid DocumentId, AccessType AccessType, UserId GrantAccessTo) : ICommand;

