using Ers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class BlockTest
    {
        private const int ClientId = 1;
        private const string Data = "Test data";
        private const string PreviousHash = "previousHash";
        private DateTime timestamp;

        [SetUp]
        public void Setup()
        {
            timestamp = DateTime.Now;
        }

        [Test]
        public void Constructor_ShouldInitializeBlockAndCalculateHash()
        {
            // Act
            var block = new Block(ClientId, timestamp, Data, PreviousHash);

            // Assert
            Assert.AreEqual(ClientId, block.ClientId);
            Assert.AreEqual(timestamp, block.Timestamp);
            Assert.AreEqual(Data, block.Data);
            Assert.AreEqual(PreviousHash, block.PreviousHash);
            Assert.IsNotNull(block.Hash);
            Assert.IsNotEmpty(block.Hash);
        }

        [Test]
        public void CalculateHash_ShouldReturnCorrectHash()
        {
            // Arrange
            var block = new Block(ClientId, timestamp, Data, PreviousHash);
            string expectedHash = block.CalculateHash();

            // Act
            string actualHash = block.CalculateHash();

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }

        [Test]
        public void MineBlock_ShouldGenerateHashWithLeadingZeros()
        {
            // Arrange
            var block = new Block(ClientId, timestamp, Data, PreviousHash);
            int leadingZeros = 3;

            // Act
            block.MineBlock(leadingZeros);

            // Assert
            Assert.IsTrue(block.Hash.StartsWith(new string('0', leadingZeros)));
            Console.WriteLine($"Block mined: {block.Hash} with Num: {block.Num}");
        }
    }
}
