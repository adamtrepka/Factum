using Factum.Modules.Documents.Application;
using Factum.Modules.Documents.Application.Documents.Queries;
using Factum.Modules.Documents.Core;
using Factum.Modules.Documents.Core.Documents.DTO;
using Factum.Modules.Documents.Infrastructure;
using Factum.Shared.Abstractions.Dispatchers;
using Factum.Shared.Abstractions.Modules;
using Factum.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Api;

internal class DocumentsModule : IModule
{
    public static readonly string DocumentsPolicyName = "documents";

    public string Name => "Documents";

    public IEnumerable<string> Policies { get; } = new[]
    {
            DocumentsPolicyName
        };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
        services.AddInfrastructure();
        services.AddApplication();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests()
           .Subscribe<GetDocument, DocumentDto>("documents/get", 
                                                (query, serviceProvider, cancellationToken) => serviceProvider.GetRequiredService<IDispatcher>()
                                                                                                              .QueryAsync(query, cancellationToken));
    }
}

