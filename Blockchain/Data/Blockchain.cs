using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class Blockchain : IBlockchain
    {
        private static Blockchain instance = null;
        private static readonly object lockObject = new object();

        public List<IBlock> Chain { get; }
        public int Digits { get; set; } = 3;


        private Blockchain()
        {
            Chain = new List<IBlock> { CreateGenesisBlock() };
        }

        public static Blockchain Instance
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

        private IBlock CreateGenesisBlock()
        {
            return new Block(0, DateTime.Now, "Genesis Block", "0");
        }

        public IBlock GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(IBlock newBlock)
        {
            Chain.Add(newBlock);
        }
    }
}
