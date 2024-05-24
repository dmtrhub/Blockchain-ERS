using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class Miner 
    {
        public string Id { get; private set; }
        public double BitcoinBalance { get; private set; } = 0;
        public List<Block> LocalBlockchain { get; set; }

        public Miner(string id)
        {
            Id = id;
            LocalBlockchain = new List<Block>(Blockchain.Instance.Chain);
        }

        public List<Miner> RegisterWithSmartContract(SmartContract smartContract)
        {
            smartContract.RegisterMiner(this);
            return smartContract.registeredMiners;
        }

        public void MineBlock(Block block)
        {
            block.MineBlock(Blockchain.Instance.Digits);
        }
    }
}
