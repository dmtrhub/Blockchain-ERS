using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Data
{
    public class Block
    {
        public int Index { get; }
        public string PreviousHash { get; }
        public string Hash { get; private set; }
        public string Data { get; }

        public Block(int index, string data, string previousHash = "")
        {
            Index = index;
            Data = data;
            PreviousHash = previousHash;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = $"{Index}{PreviousHash}{Data}";
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
