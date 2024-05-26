using Ers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class BlockchainTest
    {
        [Test]
        public void Instance_ShouldReturnSingletonInstance()
        {
            // Act
            var instance1 = Blockchain.Instance;
            var instance2 = Blockchain.Instance;

            // Assert
            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void Constructor_ShouldInitializeWithGenesisBlock()
        {
            // Act
            var blockchain = Blockchain.Instance;

            // Assert
            Assert.IsNotEmpty(blockchain.Chain);
            Assert.AreEqual(1, blockchain.Chain.Count);
            Assert.AreEqual("Genesis Block", blockchain.Chain[0].Data);
        }

        [Test]
        public void GetLatestBlock_ShouldReturnMostRecentBlock()
        {
            // Arrange
            var blockchain = Blockchain.Instance;

            // Act
            var latestBlock = blockchain.GetLatestBlock();

            // Assert
            Assert.AreEqual(blockchain.Chain[blockchain.Chain.Count - 1], latestBlock);
        }

        [Test]
        public void AddBlock_ShouldAppendNewBlockToChain()
        {
            // Arrange
            var blockchain = Blockchain.Instance;
            int initialCount = blockchain.Chain.Count;
            var newBlock = new Block(1, DateTime.Now, "New Block Data", blockchain.GetLatestBlock().Hash);

            // Act
            blockchain.AddBlock(newBlock);

            // Assert
            Assert.AreEqual(initialCount + 1, blockchain.Chain.Count);
            Assert.AreEqual(newBlock, blockchain.Chain[blockchain.Chain.Count - 1]);
        }

        [TearDown]
        public void TearDown()
        {
            typeof(Blockchain).GetField("instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).SetValue(null, null);
        }
    }
}
