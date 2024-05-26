using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class Client : IClient
    {
        public int Id { get; }

        public Client(int id)
        {
            Id = id;
        }
        public void SendData(ISmartContract smartContract, string data)
        {
            Console.WriteLine($"\nClient {Id} sending data...\n");
            smartContract.ReceiveData(this, data);
            Console.WriteLine("\n-------------------------------------------------------");
        }
    }
}
