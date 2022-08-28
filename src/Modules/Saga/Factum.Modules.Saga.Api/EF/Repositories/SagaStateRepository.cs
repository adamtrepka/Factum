using Chronicle;
using Factum.Modules.Saga.Api.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Saga.Api.EF.Repositories
{
    internal class SagaStateRepository : ISagaStateRepository
    {
        private readonly SagaDbContext _sagaDbContext;
        private readonly DbSet<SagaStateEntity> _sagas;

        public SagaStateRepository(SagaDbContext sagaDbContext)
        {
            _sagaDbContext = sagaDbContext;
            _sagas = sagaDbContext.Sagas;
        }
        public async Task<ISagaState> ReadAsync(SagaId id, Type type)
        {
            return await _sagas.FirstOrDefaultAsync(x => x.Id == id && x.TypeName == type.FullName);
        }

        public async Task WriteAsync(ISagaState state)
        {
            var entity = await _sagas.FirstOrDefaultAsync(x => x.Id == state.Id && x.TypeName == state.Type.FullName);

            if(entity is not null)
            {
                entity.Update(state.State, state.Data);
                _sagas.Update(entity);
            }
            else
            {
                entity = new SagaStateEntity(state.Id, state.Type, state.State, state.Data);
                await _sagas.AddAsync(entity);
            }

            await _sagaDbContext.SaveChangesAsync();
        }
    }
}
