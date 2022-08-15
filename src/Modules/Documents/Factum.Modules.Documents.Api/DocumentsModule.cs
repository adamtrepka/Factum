using Factum.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Documents.Api
{
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

        }

        public void Use(IApplicationBuilder app)
        {

        }
    }
}
