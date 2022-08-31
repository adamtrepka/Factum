namespace Factum.Shared.Infrastructure.Security.MerkleTree
{
    public class Node : BaseNode
    {
        public Node(BaseNode left, BaseNode right, byte[] hash)
        {
            Left = left;
            Left.Parrent = this;
            Right = right;
            Right.Parrent = this;
            Hash = hash;
        }

        public BaseNode Left { get; private set; }
        public BaseNode Right { get; private set; }
    }
}
