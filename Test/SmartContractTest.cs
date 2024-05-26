using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ers;
using Moq;

namespace Test
{
    [TestFixture]
    public class SmartContractTests
    {
        private SmartContract smartContract;
        private Mock<IClient> mockClient;
        private Mock<IMiner> mockMiner;

        [SetUp]
        public void Setup()
        {
            smartContract = SmartContract.Instance;
            mockClient = new Mock<IClient>();
            mockClient.SetupGet(c => c.Id).Returns(123);

            mockMiner = new Mock<IMiner>();
            mockMiner.SetupGet(m => m.Id).Returns("Miner1");

            smartContract.registeredClients.Clear();
            smartContract.registeredMiners.Clear();
        }

        [Test]
        public void RegisterClient_ShouldAddClientToList()
        {
            // Act
            smartContract.RegisterClient(mockClient.Object);

            // Assert
            Assert.Contains(mockClient.Object, smartContract.registeredClients);
        }

        [Test]
        public void RegisterMiner_ShouldAddMinerToList()
        {
            // Act
            smartContract.RegisterMiner(mockMiner.Object);

            // Assert
            Assert.Contains(mockMiner.Object, smartContract.registeredMiners);
        }

        [Test]
        public void ReceiveData_ShouldAssignTaskToMiner()
        {
            // Arrange
            smartContract.RegisterMiner(mockMiner.Object);

            var block = new Block(123, DateTime.Now, "Test data", "previousHash");
            mockMiner.Setup(m => m.MineBlock(It.IsAny<IBlock>())).Callback<IBlock>(b =>
            {
                b.Hash = "000abc";
            });

            // Act
            smartContract.ReceiveData(mockClient.Object, "Test data");

            // Assert
            mockMiner.Verify(m => m.MineBlock(It.IsAny<IBlock>()), Times.Once);
        }

        [Test]
        public void NotifyMiners_ShouldValidateAndConfirmBlock()
        {
            // Arrange
            var mockMiner1 = new Mock<IMiner>();
            var mockMiner2 = new Mock<IMiner>();

            mockMiner1.SetupGet(m => m.Id).Returns("Miner1");
            mockMiner2.SetupGet(m => m.Id).Returns("Miner2");

            mockMiner1.Setup(m => m.ValidateBlock(It.IsAny<IBlock>())).Returns(true);
            mockMiner2.Setup(m => m.ValidateBlock(It.IsAny<IBlock>())).Returns(true);

            smartContract.RegisterMiner(mockMiner1.Object);
            smartContract.RegisterMiner(mockMiner2.Object);

            var block = new Block(123, DateTime.Now, "Test data", "previousHash")
            {
                Hash = "000abc"
            };

            // Act
            smartContract.NotifyMiners(mockMiner1.Object, block);

            // Assert
            mockMiner2.Verify(m => m.ValidateBlock(It.IsAny<IBlock>()), Times.Once);
            mockMiner1.Verify(m => m.ConfirmBlock(It.IsAny<IBlock>()), Times.Once);
        }

        [Test]
        public void NotifyMiners_ShouldFailValidation()
        {
            // Arrange
            var mockMiner2 = new Mock<IMiner>();
            mockMiner2.SetupGet(m => m.Id).Returns("Miner2");

            smartContract.RegisterMiner(mockMiner.Object);
            smartContract.RegisterMiner(mockMiner2.Object);

            var block = new Block(123, DateTime.Now, "Test data", "previousHash")
            {
                Hash = "000abc"
            };

            mockMiner2.Setup(m => m.ValidateBlock(It.IsAny<IBlock>())).Returns(false);

            // Act
            smartContract.NotifyMiners(mockMiner.Object, block);

            // Assert
            mockMiner2.Verify(m => m.ValidateBlock(It.IsAny<IBlock>()), Times.Once);
            mockMiner.Verify(m => m.ConfirmBlock(It.IsAny<IBlock>()), Times.Never);
        }
    }
}
