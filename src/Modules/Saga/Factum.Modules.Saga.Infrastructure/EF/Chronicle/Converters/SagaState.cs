using Chronicle;

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Converters
{
    internal class SagaState : ISagaState
    {
        public SagaState(SagaId id, Type type, SagaStates state, object data)
        {
            Id = id;
            Type = type;
            State = state;
            Data = data;
        }

        public SagaId Id { get; set; }

        public Type Type { get; set; }

        public SagaStates State { get; set; }

        public object Data { get; set; }

        public void Update(SagaStates state, object data = null)
        {
            State = state;
            Data = data;
        }
    }
}
