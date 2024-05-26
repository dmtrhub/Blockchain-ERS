using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class SmartContract : ISmartContract
    {
        private static SmartContract instance = null;
        private static readonly object lockObject = new object();

        public List<IClient> registeredClients { get; } = new List<IClient>();
        public List<IMiner> registeredMiners { get; } = new List<IMiner>();

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

        public void RegisterClient(IClient client)
        {
            registeredClients.Add(client);
            Console.WriteLine($"Client {client.Id} registered.");
        }

        public void RegisterMiner(IMiner miner)
        {
            registeredMiners.Add(miner);
            Console.WriteLine($"Miner {miner.Id} registered.");
        }

        public void ReceiveData(IClient client, string data)
        {
            Console.WriteLine("Data received from client " + client.Id + ":\n\t" + data);
            IBlock newBlock = new Block(client.Id, DateTime.Now, data, Blockchain.Instance.GetLatestBlock().Hash);
            AssignTask(newBlock);
        }

        public void AssignTask(IBlock block)
        {
            IMiner selectedMiner = ChooseMiner();
            if (selectedMiner != null)
            {
                Console.WriteLine($"\nThe block is assigned to the {selectedMiner.Id}");
                selectedMiner.MineBlock(block);
            }
            else
            {
                Console.WriteLine("No miners available to assign the task.");
            }
        }

        private IMiner ChooseMiner()
        {
            if (registeredMiners.Count == 0)
            {
                return null;
            }

            Random random = new Random();
            int index = random.Next(0, registeredMiners.Count);
            return registeredMiners[index];
        }

        public void NotifyMiners(IMiner thisMiner, IBlock block)
        {
            bool valid = true;

            foreach (var miner in registeredMiners)
            {
                if (miner.Id != thisMiner.Id) 
                {
                    if (!miner.ValidateBlock(block))
                    {
                        Console.WriteLine($"Miner {miner.Id} validation failed.");
                        valid = false;
                        return;
                    }
                }
            }

            if(valid)
            {
                thisMiner.ConfirmBlock(block);
                Console.WriteLine($"The block is added to the main blockchain and the local chain of {thisMiner.Id} that validated this block of data.");
            }            
        }
    }
}
