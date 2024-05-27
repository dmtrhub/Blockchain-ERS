using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class Client : IClient
    {
        private readonly IDataHandlingService dataHandlingService;
        public int Id { get; }

        public Client(int id, IDataHandlingService _dataHandlingService)
        {
            Id = id;
            dataHandlingService = _dataHandlingService;
        }
        public void SendData(string data)
        {
            Console.WriteLine($"\nClient {Id} sending data...\n");
            dataHandlingService.ReceiveData(this, data);
            Console.WriteLine("\n-------------------------------------------------------");
        }
    }
}
