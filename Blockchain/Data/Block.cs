using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class Block
    {
        public int ClientId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Data { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public int Num { get; set; }

        public Block(int clientId, DateTime timestamp, string data, string previousHash = "")
        {
            ClientId = clientId;
            Timestamp = timestamp;
            Data = data;
            PreviousHash = previousHash;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = ClientId + Timestamp.ToString() + Data + PreviousHash + Num;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string MineBlock(int digits)
        {
            string target = new string('0', digits);
            while (Hash.Substring(0, digits) != target)
            {
                Num++;
                Hash = CalculateHash();
            }
            return this.Hash;
        }

        public override string ToString()
        {
            return $"Block\n[\n\tClient id: {ClientId}\n\tTime: {Timestamp}\n\tData: {Data}\n\tPrevious Hash: {PreviousHash}\n\tHash: {Hash}\n]\n";
        }
    }
}
