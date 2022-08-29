using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Entities
{
    internal class SagaStateEntity
    {
        public SagaStateEntity(string sagaId, string type, byte state, string data)
        {
            SagaId = sagaId;
            Type = type;
            State = state;
            Data = data;
        }

        protected SagaStateEntity()
        {

        }
        public int Id { get; set; }
        public string SagaId { get; set; }
        public string Type { get; set; }
        public byte State { get; set; }
        public string Data { get; set; }
    }
}
