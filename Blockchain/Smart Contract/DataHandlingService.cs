using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class DataHandlingService : IDataHandlingService
    {
        private readonly IGenesisBlock genesisBlock;
        private readonly ITaskAssignmentService taskAssignmentService;
        private readonly IHashCalculator hashCalculator;

        public DataHandlingService(IGenesisBlock _genesisBlock, ITaskAssignmentService _taskAssignmentService, IHashCalculator _hashCalculator)
        {
            genesisBlock = _genesisBlock;
            taskAssignmentService = _taskAssignmentService;
            hashCalculator = _hashCalculator;
        }
        public void ReceiveData(IClient client, string data)
        {
            Console.WriteLine("Data received from client " + client.Id + ":\n\t" + data);
            IBlock newBlock = new Block(client.Id, DateTime.Now, data, Blockchain.GetInstance(genesisBlock).GetLatestBlock().Hash, hashCalculator);
            taskAssignmentService.AssignTask(newBlock);
        }
    }
}
