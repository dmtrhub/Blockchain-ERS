using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class Miner : IMiner
    {
        public string Id { get; private set; }
        public double BitcoinBalance { get; private set; } = 0;
        public List<IBlock> LocalBlockchain { get; set; }

        public Miner(string id)
        {
            Id = id;
            LocalBlockchain = new List<IBlock>(Blockchain.Instance.Chain);
        }

        public List<IMiner> RegisterWithSmartContract(ISmartContract smartContract)
        {
            smartContract.RegisterMiner(this);
            return smartContract.registeredMiners;
        }

        public void MineBlock(IBlock block)
        {
            Console.WriteLine($"\nBlock mined by {Id}: " + block.MineBlock(Blockchain.Instance.Digits) + '\n');
            Notify(block);
        }

        private void Notify(IBlock block)
        {
            SmartContract.Instance.NotifyMiners(this, block);
        }

        public bool ValidateBlock(IBlock block)
        {
            return block.Hash.StartsWith(new string('0', Blockchain.Instance.Digits));
        }

        public void ConfirmBlock(IBlock block)
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
