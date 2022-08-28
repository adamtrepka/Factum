using Chronicle;
using Factum.Modules.Saga.Api.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Factum.Modules.Saga.Api.EF.Repositories
{
    internal class SagaLogger : ISagaLog
    {
        private readonly SagaDbContext _sagaDbContext;
        private readonly DbSet<SagaLogDataEntity> _logs;

        public SagaLogger(SagaDbContext sagaDbContext)
        {
            _sagaDbContext = sagaDbContext;
            _logs = sagaDbContext.Logs;
        }
        public async Task<IEnumerable<ISagaLogData>> ReadAsync(SagaId id, Type type)
        {
            return await _logs.Where(x => x.Id == id && x.Type == type).ToListAsync();
        }

        public Task WriteAsync(ISagaLogData message)
        {
            return Task.CompletedTask;
            //var entity = new SagaLogDataEntity(message.Id, message.Type, message.CreatedAt, message.Message);
            //await _logs.AddAsync(entity);
            //await _sagaDbContext.SaveChangesAsync();
        }
    }
}
