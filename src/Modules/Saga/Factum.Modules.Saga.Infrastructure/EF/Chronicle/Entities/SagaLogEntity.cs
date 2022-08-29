namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Entities
{
    internal class SagaLogEntity
    {
        public SagaLogEntity(string sagaId, string type, long createdAt, string message)
        {
            SagaId = sagaId;
            Type = type;
            CreatedAt = createdAt;
            Message = message;
        }

        protected SagaLogEntity()
        {

        }
        public int Id { get; set; }
        public string SagaId { get; set; }
        public string Type { get; set; }
        public long CreatedAt { get; set; }
        public string Message { get; set; }
    }
}
