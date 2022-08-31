using System.Collections.Generic;

namespace Factum.Shared.Infrastructure.Security.MerkleTree
{
    public class MerkleTreeResult
    {
        public MerkleTreeResult(BaseNode root, List<Leaf> leaves)
        {
            Root = root;
            Leaves = leaves;
        }

        public BaseNode Root { get; private set; }
        public List<Leaf> Leaves { get; private set; }
    }
}
