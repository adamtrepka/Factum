using Factum.Shared.Infrastructure.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Ledger.Infrastructure.EF
{
    internal class LedgerUnitOfWork : SqlServerUnitOfWork<LedgerDbContext>
    {
        public LedgerUnitOfWork(LedgerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
