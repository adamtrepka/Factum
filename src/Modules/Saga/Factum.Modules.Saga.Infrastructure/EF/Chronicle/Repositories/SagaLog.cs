using Chronicle;
using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Converters;
using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Entities;
using Microsoft.EntityFrameworkCore;

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Repositories
{
    internal class SagaLog : ISagaLog
    {
        private readonly ChronicleDbContext _dbContext;
        private readonly IChronicleTypeConverter _chronicleTypeConverter;
        private readonly DbSet<SagaLogEntity> _logs;

        public SagaLog(ChronicleDbContext chronicleDbContext, IChronicleTypeConverter chronicleTypeConverter)
        {
            _dbContext = chronicleDbContext;
            _chronicleTypeConverter = chronicleTypeConverter;
            _logs = chronicleDbContext.Logs;
        }
        public Task<IEnumerable<ISagaLogData>> ReadAsync(SagaId id, Type type)
        {
            return Task.FromResult(Enumerable.Empty<ISagaLogData>());
        }

        public async Task WriteAsync(ISagaLogData message)
        {
            var entity = _chronicleTypeConverter.ConvertToLogEntity(message);
            await _logs.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
