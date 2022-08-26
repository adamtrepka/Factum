using Factum.Shared.Abstractions.Contracts;
using Factum.Shared.Abstractions.Events;

namespace Factum.Modules.Validation.Core.Events.Externals
{
    internal record UntrustedBlockAdded(Guid BlockId) : IEvent;

    internal class UntrustedBlockAddedContract : Contract<UntrustedBlockAdded>
    {
        public UntrustedBlockAddedContract()
        {
            RequireAll();
        }
    }
}
