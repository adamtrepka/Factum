using Chronicle;
using Factum.Modules.Saga.Api.Sagas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Api.Services
{
    internal class SagaService : ISagaService
    {
        private readonly ISagaStateRepository _sagaStateRepository;

        public SagaService(ISagaStateRepository sagaStateRepository)
        {
            _sagaStateRepository = sagaStateRepository;
        }

        public async Task<string> GetSagaStatus(SagaId id)
        {
            var state = await _sagaStateRepository.ReadAsync(id, typeof(NewBlockSaga));
            return state?.State.ToString() ?? "NotFound";
        }
    }
}
