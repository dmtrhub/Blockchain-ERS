using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Data
{
    public class Blockchain
    {
        private Blockchain instance = null;
        private readonly object lockObject = new object();

        public IList<Block> Chain { get; }
        public int Digits { get; set; } = 3;

        private Blockchain()
        {
            Chain = new List<Block> { CreateGenesisBlock() };
        }

        public Blockchain Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new Blockchain();
                    }
                    return instance;
                }
            }
        }

        private Block CreateGenesisBlock()
        {
            return new Block(0, DateTime.Now, "Genesis Block", "0");
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block newBlock)
        {
            newBlock.PreviousHash = GetLatestBlock().Hash;
            Chain.Add(newBlock);
        }
    }
}
