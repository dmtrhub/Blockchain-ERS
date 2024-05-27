using Ers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ers
{
    public class BlockValidator : IBlockValidator
    {
        public bool ValidateBlock(IBlock block)
        {
            return block.Hash.StartsWith(new string('0', block.Digits));
        }
    }
}
