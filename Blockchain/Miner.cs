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
            Console.WriteLine($"\nBlock mined by {Id}: " + block.MineBlock(Blockchain.Instance.Digits) + '\n');
            Notify(block);
        }

        private void Notify(Block block)
        {
            SmartContract.Instance.NotifyMiners(this, block);
        }

        public bool ValidateBlock(Block block)
        {
            return block.Hash.StartsWith(new string('0', Blockchain.Instance.Digits));
        }

        public void ConfirmBlock(Block block)
        {
            Blockchain.Instance.AddBlock(block);
            LocalBlockchain.Add(block);
            BitcoinBalance += 1;
        }
        public override string ToString()
        {
            return $"Miner id: {Id}, Bitcoin Balance: {BitcoinBalance}$, Local Blockchain Length: {LocalBlockchain.Count}";
        }
    }
}
