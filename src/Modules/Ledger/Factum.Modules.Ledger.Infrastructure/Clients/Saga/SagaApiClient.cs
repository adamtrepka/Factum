using Factum.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Infrastructure.Clients.Saga
{
    internal class SagaApiClient : ISagaApiClient
    {
        private readonly IModuleClient _moduleClient;

        public SagaApiClient(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient ?? throw new ArgumentNullException(nameof(moduleClient));
        }

        public Task<string> Get(string id) => _moduleClient.SendAsync<string>("saga/get", id);
    }
}
