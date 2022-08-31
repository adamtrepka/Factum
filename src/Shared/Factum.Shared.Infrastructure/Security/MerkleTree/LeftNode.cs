namespace Factum.Shared.Infrastructure.Security.MerkleTree
{
    public class LeftNode : ProofNode
    {
        public LeftNode(byte[] hash)
        {
            Hash = hash;
        }
    }
}
