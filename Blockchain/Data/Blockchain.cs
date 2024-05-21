using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Data
{
    public class Blockchain
    {
        public List<Block> Chain { get; }

        private Blockchain()
        {
            Chain = new List<Block>();
        }

        public void AddBlock(Block newBlock)
        {
            Chain.Add(newBlock);
        }
    }
}
