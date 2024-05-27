using Ers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class MiningService : IMiningService
    {
        private readonly IHashCalculator hashCalculator;
        private readonly INotificationService notificationService;

        public MiningService(IHashCalculator _hashCalculator, INotificationService _notificationService)
        {
            hashCalculator = _hashCalculator;
            notificationService = _notificationService;
        }

        public void MineBlock(IBlock block, IMiner miner)
        {
            string target = new string('0', block.Digits);
            string data = $"{block.ClientId}{block.Timestamp}{block.Data}{block.PreviousHash}";

            while (true)
            {
                block.Num++;
                block.Hash = hashCalculator.CalculateHash($"{data}{block.Num}");

                if (block.Hash.StartsWith(target))
                {
                    Console.WriteLine($"Block mined: {block.Hash}");
                    break;
                }
            }
            notificationService.NotifyMiners(miner, block);
        }
    }
}
