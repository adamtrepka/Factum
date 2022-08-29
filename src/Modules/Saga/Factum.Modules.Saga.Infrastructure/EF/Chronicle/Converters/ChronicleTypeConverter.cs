using Chronicle;
using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Entities;
using Factum.Shared.Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Converters
{
    internal class ChronicleTypeConverter : IChronicleTypeConverter
    {
        private readonly IJsonSerializer _jsonSerializer;

        public ChronicleTypeConverter(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
        }

        public ISagaState ConvertToSagaState(SagaStateEntity entity)
        {
            if (entity is null) return null;

            var sagaType = GetTypeByName(entity.Type);

            var sagaGenericDataType = sagaType.GetInterfaces()
                                        .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISaga<>))?
                                        .GetGenericArguments()?
                                        .FirstOrDefault();

            var sagaData = _jsonSerializer.Deserialize(entity.Data, sagaGenericDataType);
            return new SagaState(entity.SagaId, sagaType, (SagaStates)entity.State, sagaData);
        }

        public SagaStateEntity ConvertToStateEntity(ISagaState sagaState)
        {
            var data = _jsonSerializer.SerializeToJsonString(sagaState.Data);
            return new SagaStateEntity(sagaState.Id, sagaState.Type.FullName, (byte)sagaState.State, data);
        }

        public void UpdateSagaStateEntity(ISagaState sagaState, SagaStateEntity entity)
        {
            entity.SagaId = sagaState.Id;
            entity.Type = sagaState.Type.FullName;
            entity.State = (byte)sagaState.State;
            entity.Data = _jsonSerializer.SerializeToJsonString(sagaState.Data);
        }

        public SagaLogEntity ConvertToLogEntity(ISagaLogData sagaLogData)
        {
            var message = _jsonSerializer.SerializeToJsonString(sagaLogData.Message);
            return new SagaLogEntity(sagaLogData.Id, sagaLogData.Type.FullName, sagaLogData.CreatedAt, message);
        }

        private Type GetTypeByName(string typeName)
            => AppDomain.CurrentDomain
                        .GetAssemblies()
                        .SelectMany(x => x.GetTypes())
                        .FirstOrDefault(x => x.FullName
                                              .Equals(typeName,
                                                      StringComparison.InvariantCultureIgnoreCase));
    }
}
