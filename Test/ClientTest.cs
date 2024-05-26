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
    public class ClientTest
    {
        private Mock<ISmartContract> mockSmartContract;

        [SetUp]
        public void Setup()
        {
            mockSmartContract = new Mock<ISmartContract>();
        }

        [Test]
        public void SendData_ShouldCallReceiveDataOnSmartContract()
        {
            // Arrange
            int clientId = 1;
            string data = "Test data";
            var client = new Client(clientId);

            // Act
            client.SendData(mockSmartContract.Object, data);

            // Assert
            mockSmartContract.Verify(sc => sc.ReceiveData(client, data), Times.Once);
        }
    }
}
