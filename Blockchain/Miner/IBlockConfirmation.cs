using Ers;

namespace Ers
{
    public interface IBlockConfirmation
    {
        void ConfirmBlock(IBlock block, IMiner miner);
    }
}