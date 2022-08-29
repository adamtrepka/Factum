using Chronicle;
using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Entities;

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Converters
{
    internal interface IChronicleTypeConverter
    {
        SagaLogEntity ConvertToLogEntity(ISagaLogData sagaLogData);
        ISagaState ConvertToSagaState(SagaStateEntity entity);
        SagaStateEntity ConvertToStateEntity(ISagaState sagaState);
        void UpdateSagaStateEntity(ISagaState sagaState, SagaStateEntity entity);
    }
}