using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class NotificationService : INotificationService
    {
        private readonly IRegistrationService registrationService;
        private readonly IBlockConfirmation blockConfirmation;
        public NotificationService(IRegistrationService _registrationService, IBlockConfirmation _blockConfirmation)
        {
            registrationService = _registrationService;
            blockConfirmation = _blockConfirmation;
        }
        public void NotifyMiners(IMiner thisMiner, IBlock block)
        {
            bool valid = true;

            foreach (var miner in registrationService.registeredMiners)
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

            if (valid)
            {
                blockConfirmation.ConfirmBlock(block, thisMiner);
                Console.WriteLine($"The block is added to the main blockchain and the local chain of {thisMiner.Id} that validated this block of data.");
            }
        }
    }

}
