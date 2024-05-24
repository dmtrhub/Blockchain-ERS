using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class SmartContract
    {
        private static SmartContract instance = null;
        private static readonly object lockObject = new object();

        public List<Client> registeredClients { get; } = new List<Client>();
        public List<Miner> registeredMiners { get; } = new List<Miner>();

        private SmartContract() { }

        public static SmartContract Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SmartContract();
                    }
                    return instance;
                }
            }
        }

        public void RegistedClient(Client client)
        {
            registeredClients.Add(client);
            Console.WriteLine($"Client {client.Id} registered.");
        }

        public void RegisterMiner(Miner miner)
        {
            registeredMiners.Add(miner);
            Console.WriteLine($"Miner {miner.Id} registered.");
        }

        public Block ReceiveData(Client client, string data)
        {
            Console.WriteLine("Data received from client " + client.Id + ":\n\t" + data);
            Block newBlock = new Block(client.Id, DateTime.Now, data);
            AssignTask(newBlock);
            return newBlock;
        }

        public void AssignTask(Block block) { }
    }
}
