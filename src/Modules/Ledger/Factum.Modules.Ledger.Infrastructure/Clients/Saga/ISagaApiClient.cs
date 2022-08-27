
namespace Factum.Modules.Ledger.Infrastructure.Clients.Saga
{
    internal interface ISagaApiClient
    {
        Task<string> Get(string id);
    }
}