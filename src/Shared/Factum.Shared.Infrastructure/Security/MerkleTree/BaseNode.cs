using System.Text;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.Security.MerkleTree
{
    public abstract class BaseNode
    {
        public Node Parrent { get; set; }
        public byte[] Hash { get; set; }
    }
}
