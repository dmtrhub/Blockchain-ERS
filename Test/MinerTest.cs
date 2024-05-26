using Ers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class MinerTest
    {
        private Miner miner;
        private Mock<ISmartContract> mockSmartContract;
        private List<IMiner> registeredMiners;

        [SetUp]
        public void Setup()
        {
            mockSmartContract = new Mock<ISmartContract>();
            registeredMiners = new List<IMiner>();
            mockSmartContract.Setup(s => s.RegisterMiner(It.IsAny<IMiner>()))
                             .Callback<IMiner>(m => registeredMiners.Add(m));
            mockSmartContract.Setup(s => s.registeredMiners).Returns(registeredMiners);

            miner = new Miner("miner1");
        }

        [Test]
        public void Test_RegisterWithSmartContract()
        {
            // Act
            var miners = miner.RegisterWithSmartContract(mockSmartContract.Object);

            // Assert
            Assert.Contains(miner, miners);
            Assert.AreEqual(1, miners.Count);
            Assert.AreEqual(miner, miners[0]);
            mockSmartContract.Verify(s => s.RegisterMiner(miner), Times.Once);
        }

        [Test]
        public void Test_MineBlock()
        {
            // Arrange
            var mockBlock = new Mock<IBlock>();
            mockBlock.Setup(b => b.Hash).Returns(new string('0', Blockchain.Instance.Digits));

            // Act
            miner.MineBlock(mockBlock.Object);

            // Assert
            mockBlock.Verify(b => b.MineBlock(Blockchain.Instance.Digits), Times.Once);
        }

        [Test]
        public void Test_ValidateBlock_ValidBlock()
        {
            // Arrange
            var mockBlock = new Mock<IBlock>();
            mockBlock.Setup(b => b.Hash).Returns(new string('0', Blockchain.Instance.Digits));

            // Act
            bool isValid = miner.ValidateBlock(mockBlock.Object);

            // Assert
            Assert.IsTrue(isValid);
        }

        [Test]
        public void Test_ValidateBlock_InvalidBlock()
        {
            // Arrange
            var mockBlock = new Mock<IBlock>();
            mockBlock.Setup(b => b.Hash).Returns("invalidhash");

            // Act
            bool isValid = miner.ValidateBlock(mockBlock.Object);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        public void Test_ConfirmBlock()
        {
            // Arrange
            var mockBlock = new Mock<IBlock>();
            mockBlock.Setup(b => b.Hash).Returns(new string('0', Blockchain.Instance.Digits));

            // Act
            miner.ConfirmBlock(mockBlock.Object);

            // Assert
            Assert.Contains(mockBlock.Object, miner.LocalBlockchain);
            Assert.AreEqual(1, miner.BitcoinBalance);
        }
    }
}
