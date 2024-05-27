using Ers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class GenesisBlock : IGenesisBlock
    {
        private readonly IHashCalculator hashCalculator;

        public GenesisBlock(IHashCalculator _hashCalculator)
        {
            hashCalculator = _hashCalculator;
        }

        public IBlock CreateGenesisBlock()
        {
            return new Block(0, DateTime.Now, "Genesis Block", "0", hashCalculator);
        }
    }
}
