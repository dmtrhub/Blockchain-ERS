using Ers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class Miner : IMiner
    {
        private readonly IMiningService _miningService;
        private readonly IBlockValidator _blockValidator;
        private readonly IBlockConfirmation _blockConfirmationService;

        public string Id { get; set; }
        public double BitcoinBalance { get; set; } = 0;
        public List<IBlock> LocalChain { get; set; } = new List<IBlock>();

        public Miner(string id, IMiningService miningService, IBlockValidator blockValidator, IBlockConfirmation blockConfirmationService)
        {
            Id = id;
            _miningService = miningService;
            _blockValidator = blockValidator;
            _blockConfirmationService = blockConfirmationService;
        }

        public void MineBlock(IBlock block, IMiner miner)
        {
            _miningService.MineBlock(block, this);
        }

        public bool ValidateBlock(IBlock block)
        {
            return _blockValidator.ValidateBlock(block);
        }

        public void ConfirmBlock(IBlock block, IMiner miner)
        {
            _blockConfirmationService.ConfirmBlock(block, this);
        }

        public override string ToString()
        {
            return $"Miner id: {Id}, Bitcoin Balance: {BitcoinBalance}$, Local Blockchain Length: {LocalChain.Count}";
        }
    }
}

