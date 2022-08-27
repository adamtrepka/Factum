using Chronicle;

namespace Factum.Modules.Saga.Api.Services
{
    internal interface ISagaService
    {
        Task<string> GetSagaStatus(SagaId id);
    }
}