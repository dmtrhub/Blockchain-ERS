using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Data
{
    public class Blockchain
    {
        private static Blockchain instance;
        public static Blockchain Instance => instance ??= new Blockchain();
        public List<Block> Chain { get; }

        private Blockchain()
        {
            Chain = new List<Block> { CreateGenesisBlock() };
        }

        private Block CreateGenesisBlock()
        {
            return new Block(0, "Genesis Block", "0");
        }

        public Block GetLatestBlock()
        {
            return Chain[^1];
        }

        public void AddBlock(Block newBlock)
        {
            Chain.Add(newBlock);
        }
    }
}
