namespace Factum.Shared.Infrastructure.Security.MerkleTree
{
    public class Leaf : BaseNode
    {
        public Leaf(byte[] hash)
        {
            Hash = hash;
        }
    }
}
