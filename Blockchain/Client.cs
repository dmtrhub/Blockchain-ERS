using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    public class Client
    {
        private SmartContract smartContract;
        public string Id { get; }

        public Client(string id, SmartContract smartContract)
        {
            Id = id;
            this.smartContract = smartContract;
        }

        public void SendData(string data)
        {
            Console.WriteLine($"\nClient {Id} sending data...\n");
        }
    }
}
