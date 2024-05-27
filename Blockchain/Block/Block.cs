using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class Block : IBlock
    {
        public int ClientId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Data { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public int Num { get; set; }
        public int Digits { get; set; } = 3;

        public Block(int clientId, DateTime timestamp, string data, string previousHash, IHashCalculator hashCalculator)
        {
            ClientId = clientId;
            Timestamp = timestamp;
            Data = data;
            PreviousHash = previousHash;
            Hash = hashCalculator.CalculateHash($"{clientId}{timestamp}{data}{previousHash}{Num}");
        }

        public override string ToString()
        {
            return $"Block\n[\n\tClient id: {ClientId}\n\tTime: {Timestamp}\n\tData: {Data}\n\tPrevious Hash: {PreviousHash}\n\tHash: {Hash}\n]\n";
        }
    }
}
