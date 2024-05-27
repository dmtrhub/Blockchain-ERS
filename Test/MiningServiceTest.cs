using NUnit.Framework;
using Moq;
using System;
using Ers;

namespace Test
{
    [TestFixture]
    public class MiningServiceTests
    {
        [Test]
        public void MineBlock_Success()
        {
            // Arrange
            Mock<IHashCalculator> mockHashCalculator = new Mock<IHashCalculator>();
            Mock<INotificationService> mockNotificationService = new Mock<INotificationService>();

            IBlock block = new Block(1, DateTime.Now, "Data", "PrevHash", mockHashCalculator.Object);
            IMiner miner = new Miner("Miner1", Mock.Of<IMiningService>(), Mock.Of<IBlockValidator>(), Mock.Of<IBlockConfirmation>());

            MiningService miningService = new MiningService(mockHashCalculator.Object, mockNotificationService.Object);

            mockHashCalculator.Setup(x => x.CalculateHash(It.IsAny<string>())).Returns("00000");

            // Act
            miningService.MineBlock(block, miner);

            // Assert
            Assert.AreEqual("00000", block.Hash);
            mockNotificationService.Verify(x => x.NotifyMiners(miner, block), Times.Once);
        }
    }
}
