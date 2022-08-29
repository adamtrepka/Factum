using Chronicle;
using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Converters;
using Factum.Modules.Saga.Infrastructure.EF.Chronicle.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Infrastructure.EF.Chronicle.Repositories
{
    internal class SagaStateRepository : ISagaStateRepository
    {
        private readonly ChronicleDbContext _dbContext;
        private readonly IChronicleTypeConverter _chronicleTypeConverter;
        private readonly DbSet<SagaStateEntity> _sagas;

        public SagaStateRepository(ChronicleDbContext dbContext, IChronicleTypeConverter chronicleTypeConverter)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _chronicleTypeConverter = chronicleTypeConverter ?? throw new ArgumentNullException(nameof(chronicleTypeConverter));
            _sagas = dbContext.Sagas;
        }
        public async Task<ISagaState> ReadAsync(SagaId id, Type type)
        {
            var entity = await _sagas.SingleOrDefaultAsync(x => x.SagaId == id && x.Type == type.FullName);
            return _chronicleTypeConverter.ConvertToSagaState(entity);
        }

        public async Task WriteAsync(ISagaState state)
        {
            var entity = await _sagas.FirstOrDefaultAsync(x => x.SagaId == state.Id && x.Type == state.Type.FullName);

            if (entity is not null)
            {

                _chronicleTypeConverter.UpdateSagaStateEntity(state, entity);
                _sagas.Update(entity);
            }
            else
            {
                var newEntity = _chronicleTypeConverter.ConvertToStateEntity(state);
                await _sagas.AddAsync(newEntity);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
