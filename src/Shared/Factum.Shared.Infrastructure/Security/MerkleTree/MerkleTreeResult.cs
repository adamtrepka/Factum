using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public List<ProofNode> GetProof(byte[] hash)
        {
            var leaf = Leaves.FirstOrDefault(x => x.Hash.SequenceEqual(hash));
            var proofItems = new List<ProofNode>();
            var node = (BaseNode)leaf;
            while(node.Parrent != null)
            {
                if(node.Parrent.Left == node)
                {
                    proofItems.Add(new LeftNode(node.Parrent.Right.Hash));
                }
                else
                {
                    proofItems.Add(new RightNode(node.Parrent.Left.Hash));
                }
                node = node.Parrent;
            }
            return proofItems;
        }
    }
}
