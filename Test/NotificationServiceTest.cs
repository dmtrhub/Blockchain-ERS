using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using Ers;

namespace Test
{
    [TestFixture]
    public class NotificationServiceTests
    {
        [Test]
        public void NotifyMiners_InvalidBlock()
        {
            // Arrange
            var mockRegistrationService = new Mock<IRegistrationService>();
            var mockBlockConfirmation = new Mock<IBlockConfirmation>();

            IMiner thisMiner = new Miner("Miner1", Mock.Of<IMiningService>(), Mock.Of<IBlockValidator>(), Mock.Of<IBlockConfirmation>());
            IBlock invalidBlock = new Block(1, DateTime.Now, "Data", "PrevHash", Mock.Of<IHashCalculator>());

            mockRegistrationService.Setup(x => x.registeredMiners).Returns(new List<IMiner>
            {
                new Miner("Miner2", Mock.Of<IMiningService>(), Mock.Of<IBlockValidator>(), Mock.Of<IBlockConfirmation>())
            });

            NotificationService notificationService = new NotificationService(mockRegistrationService.Object, mockBlockConfirmation.Object);

            // Act
            notificationService.NotifyMiners(thisMiner, invalidBlock);

            // Assert
            mockBlockConfirmation.Verify(x => x.ConfirmBlock(invalidBlock, thisMiner), Times.Never);
        }
    }
}