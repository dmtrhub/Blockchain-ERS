using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class TaskAssignmentService : ITaskAssignmentService
    {
        private readonly Random _random = new Random();

        private readonly IRegistrationService registrationService;
        private readonly IMiningService miningService;

        public TaskAssignmentService(IRegistrationService _registrationService, IMiningService _miningService)
        {
            registrationService = _registrationService;
            miningService = _miningService;
        }

        public void AssignTask(IBlock block)
        {
            if (registrationService.registeredMiners.Count == 0)
            {
                Console.WriteLine("No miners available to assign the task.");
                return;
            }

            int index = _random.Next(0, registrationService.registeredMiners.Count);
            IMiner selectedMiner = registrationService.registeredMiners[index];

            if (selectedMiner != null)
            {
                Console.WriteLine($"\nThe block is assigned to the {selectedMiner.Id}");
                miningService.MineBlock(block, selectedMiner);
            }
            else
            {
                Console.WriteLine("No miners available to assign the task.");
            }
        }
    }
}
