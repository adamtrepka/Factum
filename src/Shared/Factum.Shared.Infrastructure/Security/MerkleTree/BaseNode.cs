using System.Text;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.Security.MerkleTree
{
    public abstract class BaseNode
    {
        public BaseNode Parrent { get; set; }
        public byte[] Hash { get; set; }
    }
}
