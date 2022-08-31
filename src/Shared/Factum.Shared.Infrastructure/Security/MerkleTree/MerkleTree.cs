using Factum.Shared.Infrastructure.Security.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factum.Shared.Infrastructure.Security.MerkleTree
{

    internal class MerkleTree : IMerkleTree
    {
        private readonly IHasher _hasher;

        public MerkleTree(IHasher hasher)
        {
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }
        private BaseNode Root { get; set; }
        private List<Leaf> Leaves { get; set; }

        public MerkleTreeResult BuildTree(IEnumerable<byte[]> hashes)
        {
            Leaves = hashes.Select(x => new Leaf(x)).ToList();
            BuildTree(Leaves);
            return new MerkleTreeResult(Root, Leaves);
        }

        private void BuildTree(IEnumerable<BaseNode> nodes)
        {
            if (nodes.Count() == 1)
            {
                Root = nodes.First();
                return;
            }

            var nextLevel = new List<BaseNode>();

            var chunks = nodes.Chunk(2);
            var pairs = chunks.Where(x => x.Length == 2);
            var noPair = pairs.Where(x => x.Length == 1);

            foreach (var pair in pairs)
            {
                var left = pair[0];
                var right = pair[1];
                var concatHashes = left.Hash.Concat(right.Hash).ToArray();
                var nodeHash = _hasher.Hash(concatHashes);
                var node = new Node(left, right, nodeHash);
                nextLevel.Add(node);
            }

            foreach (var single in noPair)
            {
                var left = single[0];
                var right = single[0];
                var concatHashes = left.Hash.Concat(right.Hash).ToArray();
                var nodeHash = _hasher.Hash(concatHashes);
                var node = new Node(left, right, nodeHash);
                nextLevel.Add(node);
            }

            BuildTree(nextLevel);
        }

        public bool Validate(List<ProofNode> proofNodes, byte[] hash, byte[] root)
        {
            var proofHash = hash;
            foreach(var node in proofNodes)
            {
                if(node is RightNode)
                {
                    var concat = node.Hash.Concat(proofHash).ToArray();
                    proofHash = _hasher.Hash(concat);
                }
                else if (node is LeftNode)
                {
                    var concat = proofHash.Concat(node.Hash).ToArray();
                    proofHash = _hasher.Hash(concat);
                }
                else
                {
                    return false;
                }
            }
            return proofHash.SequenceEqual(root);
        }
    }
}
