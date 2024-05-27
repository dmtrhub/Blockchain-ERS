using Ers;

namespace Ers
{
    public interface IBlockValidator
    {
        bool ValidateBlock(IBlock block);
    }
}