using System.Collections.Generic;

namespace Factum.Shared.Infrastructure.Security.MerkleTree
{
    public interface IMerkleTree
    {
        MerkleTreeResult BuildTree(IEnumerable<byte[]> hashes);
        bool Validate(List<ProofNode> proofNodes, byte[] hash, byte[] root);
    }
}