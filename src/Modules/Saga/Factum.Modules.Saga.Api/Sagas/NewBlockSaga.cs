using Chronicle;
using Factum.Modules.Saga.Api.Messages;
using Factum.Modules.Saga.Api.Messages.External;
using Factum.Shared.Abstractions.Messaging;

namespace Factum.Modules.Saga.Api.Sagas
{
    internal class NewBlockSagaData
    {
        public Guid BlockId { get; set; }
        public int BlockVerified { get; set; }
        public int Confirmed { get; set; }
        public int RequiredConfirmations { get; set; }
    }
    internal sealed class NewBlockSaga : Saga<NewBlockSagaData>, ISagaStartAction<BlockAdded>, ISagaAction<BlockValidated>, ISagaAction<BlockRejected>
    {
        private readonly IMessageBroker _messageBroker;

        public NewBlockSaga(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(BlockAdded message, ISagaContext context)
        {
            Data.BlockId = message.NewBlockId;
            Data.Confirmed = message.Confirmed;
            Data.RequiredConfirmations = message.RequiredConfirmations;

            await _messageBroker.PublishAsync(new UntrustedBlockAdded(Data.BlockId));
        }
        public Task CompensateAsync(BlockAdded message, ISagaContext context) => Task.CompletedTask;
        public async Task HandleAsync(BlockValidated message, ISagaContext context)
        {
            Data.BlockVerified++;
            if (Data.BlockVerified < Data.RequiredConfirmations)
            {
                await _messageBroker.PublishAsync(new UntrustedBlockAdded(Data.BlockId));
            }
            else
            {
                await _messageBroker.PublishAsync(new SagaComplated());
                await CompleteAsync();
            }
        }
        public Task CompensateAsync(BlockValidated message, ISagaContext context) => Task.CompletedTask;
        public async Task HandleAsync(BlockRejected message, ISagaContext context)
        {
            await _messageBroker.PublishAsync(new SagaRejected());
            await CompleteAsync();
        }
        public Task CompensateAsync(BlockRejected message, ISagaContext context) => Task.CompletedTask;



        public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
            {
                BlockAdded m => m.NewBlockId.ToString(),
                BlockValidated m => m.BlockId.ToString(),
                BlockRejected m => m.BlockId.ToString(),
                _ => base.ResolveId(message, context)
            };
    }
}
