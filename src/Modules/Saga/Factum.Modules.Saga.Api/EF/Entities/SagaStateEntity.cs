using Chronicle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Api.EF.Entities
{
    internal class SagaLogDataEntity : ISagaLogData
    {
        public SagaLogDataEntity(SagaId id, Type type, long createdAt, object message)
        {
            Id = id;
            Type = type;
            CreatedAt = createdAt;
            Message = message;
        }

        protected SagaLogDataEntity()
        {

        }
        public int PrimaryKey { get; set; }

        public SagaId Id { get; set; }

        public Type Type { get; set; }

        public long CreatedAt { get; set; }

        public object Message { get; set; }
    }

    internal class SagaStateEntity : ISagaState
    {
        public SagaStateEntity(SagaId id, Type type, SagaStates state, object data)
        {
            Id = id;
            TypeName = type.FullName;
            State = state;
            DataJson = JsonConvert.SerializeObject(data);
        }

        protected SagaStateEntity()
        {

        }

        public int PrimaryKey { get; set; }
        public SagaId Id { get; set; }

        public Type Type => Assembly.GetExecutingAssembly().GetType(TypeName);

        public SagaStates State { get; set; }
        public object Data
        {
            get
            {
                var sagaInterface = Type.GetInterfaces()
                                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISaga<>));
                var sagaGenericDataType = sagaInterface?.GetGenericArguments()?.FirstOrDefault();
                var currPayLoad = JsonConvert.DeserializeObject(DataJson, sagaGenericDataType);
                return currPayLoad;
            }
        }

        public string DataJson { get; set; }
        public string TypeName { get; set; }

        public void Update(SagaStates state, object data = null)
        {
            State = state;
            DataJson = JsonConvert.SerializeObject(data);
        }

    }
}
