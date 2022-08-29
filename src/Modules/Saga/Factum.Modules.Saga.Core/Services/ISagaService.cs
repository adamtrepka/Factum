
using Chronicle;

namespace Factum.Modules.Saga.Core.Services
{
    internal interface ISagaService
    {
        Task<string> GetSagaStatus(SagaId id);
    }
}