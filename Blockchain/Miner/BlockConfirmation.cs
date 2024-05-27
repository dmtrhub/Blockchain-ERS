using Ers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class BlockConfirmation : IBlockConfirmation
    {
        private readonly IGenesisBlock genesisBlock;

        public BlockConfirmation(IGenesisBlock _genesiBlock)
        {
            genesisBlock = _genesiBlock;
        }
        public void ConfirmBlock(IBlock block, IMiner miner)
        {
            Blockchain.GetInstance(genesisBlock).AddBlock(block);
            miner.LocalChain.Add(block);
            miner.BitcoinBalance += 1;
        }
    }
}
