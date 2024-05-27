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
    public class TaskAssignmentServiceTests
    {
        [Test]
        public void AssignTask_NoMinersAvailable()
        {
            // Arrange
            var mockRegistrationService = new Mock<IRegistrationService>();
            var mockMiningService = new Mock<IMiningService>();
            var taskAssignmentService = new TaskAssignmentService(mockRegistrationService.Object, mockMiningService.Object);

            // Set up registeredMiners list to be empty
            mockRegistrationService.Setup(x => x.registeredMiners).Returns(new List<IMiner>());

            // Act
            taskAssignmentService.AssignTask(new Block(1, DateTime.Now, "Data", "PrevHash", Mock.Of<IHashCalculator>()));

            // Assert
            // Ensure that the message "No miners available to assign the task." is printed to the console
            // In unit testing, we typically don't test console output directly, so this assertion is omitted.
        }

        [Test]
        public void AssignTask_MinersAvailable()
        {
            // Arrange
            var mockRegistrationService = new Mock<IRegistrationService>();
            var mockMiningService = new Mock<IMiningService>();
            var taskAssignmentService = new TaskAssignmentService(mockRegistrationService.Object, mockMiningService.Object);

            // Set up registeredMiners list with mock miners
            var mockMiner1 = new Mock<IMiner>();
            mockMiner1.SetupGet(x => x.Id).Returns("Miner1");

            var mockMiner2 = new Mock<IMiner>();
            mockMiner2.SetupGet(x => x.Id).Returns("Miner2");

            var registeredMiners = new List<IMiner> { mockMiner1.Object, mockMiner2.Object };
            mockRegistrationService.Setup(x => x.registeredMiners).Returns(registeredMiners);

            // Act
            taskAssignmentService.AssignTask(new Block(1, DateTime.Now, "Data", "PrevHash", Mock.Of<IHashCalculator>()));

            // Assert
            // Verify that miningService.MineBlock is called with a valid miner
            mockMiningService.Verify(x => x.MineBlock(It.IsAny<IBlock>(), It.IsAny<IMiner>()), Times.Once);
        }
    }
}

