using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class RegistrationService : IRegistrationService
    {
        public List<IClient> registeredClients { get; } = new List<IClient>();
        public List<IMiner> registeredMiners { get; } = new List<IMiner>();

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
    }
}
