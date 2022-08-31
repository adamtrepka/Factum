using Factum.Shared.Infrastructure.Security.Encryption;
using Factum.Shared.Infrastructure.Security.MerkleTree;
using Factum.Shared.Infrastructure.Serialization;

namespace Factum.Shared.Tests
{
    public class MerkleTreeTests
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHasher _hasher;
        private readonly IMerkleTree _merkleTree;

        public MerkleTreeTests()
        {
            _jsonSerializer = new SystemTextJsonSerializer();
            _hasher = new Hasher(_jsonSerializer);
            _merkleTree = new MerkleTree(_hasher);
        }

        [Fact]
        public void MerkleRootHashCalculatedCorrectlyForSingleHash()
        {
            var inputData = new
            {
                Data = "Input"
            };
            var inputDataHash = _hasher.Hash(inputData);
            var input = new[] { inputDataHash };
            var result = _merkleTree.BuildTree(input);

            Assert.True(inputDataHash.SequenceEqual(result.Root.Hash));
        }

        [Fact]
        public void MerkleRootHashCalculatedCorrectlyForTwoHashes()
        {
            var hash1 = _hasher.Hash((object)"Input 1");
            var hash2 = _hasher.Hash((object)"Input 2");

            var hashesConcat = hash1.Concat(hash2);
            var hashesConcatHash = _hasher.Hash(hashesConcat);

            var input = new[] { hash1, hash2 };
            var result = _merkleTree.BuildTree(input);

            Assert.True(hashesConcatHash.SequenceEqual(result.Root.Hash));
        }

        [Fact]
        public void MerkleRootHashCalculatedCorectly()
        {
            var hashes = Enumerable.Range(0, 1024).Select(x => _hasher.Hash(x));
            var result = _merkleTree.BuildTree(hashes);

            Assert.NotNull(result.Root);
            Assert.Equal(1024, result.Leaves.Count);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(64)]
        [InlineData(128)]
        [InlineData(256)]
        [InlineData(512)]
        [InlineData(1024)]
        public void MerkleProofTest(int length)
        {
            var hashes = Enumerable.Range(0, length).Select(x => _hasher.Hash(x));
            var result = _merkleTree.BuildTree(hashes);

            var lastHash = hashes.Last();

            var proof = result.GetProof(lastHash);
            var isValid = _merkleTree.Validate(proof, lastHash, result.Root.Hash);
        }

        [Fact]
        public void Merkle_Root_hash_should_be_diffrent_if_change_order_of_input_hashes()
        {
            var hashes = Enumerable.Range(0, 64).Select(x => _hasher.Hash(x)).ToList();
            var randomHashes = hashes.OrderBy(x => Random.Shared.Next(hashes.Count())).ToList();

            var firstTreeResult = _merkleTree.BuildTree(hashes);
            var secondTreeResult = _merkleTree.BuildTree(randomHashes);

            Assert.False(firstTreeResult.Root.Hash.SequenceEqual(secondTreeResult.Root.Hash));
        }
    }
}