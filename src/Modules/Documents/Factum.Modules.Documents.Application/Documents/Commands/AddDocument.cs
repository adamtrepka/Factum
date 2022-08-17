using Factum.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Application.Documents.Commands;

internal record AddDocument(Guid documentId, IFormFile file) : ICommand;


