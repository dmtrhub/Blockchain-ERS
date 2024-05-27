using Ers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class Blockchain : IBlockchain
    {
        private static Blockchain _instance;
        private static readonly object _lock = new object();

        private readonly List<IBlock> _chain;
        private readonly IGenesisBlock _genesisBlock;

        private Blockchain(IGenesisBlock genesisBlock)
        {
            _chain = new List<IBlock>();
            _genesisBlock = genesisBlock;
            InitializeBlockchain();
        }

        public static Blockchain GetInstance(IGenesisBlock genesisBlock)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Blockchain(genesisBlock);
                    }
                }
            }
            return _instance;
        }

        private void InitializeBlockchain()
        {
            _chain.Add(_genesisBlock.CreateGenesisBlock());
        }

        public void AddBlock(IBlock newBlock)
        {
            _chain.Add(newBlock);
        }

        public List<IBlock> GetChain()
        {
            return _chain;
        }

        public IBlock GetLatestBlock()
        {
            return _chain[_chain.Count - 1];
        }
    }
}
