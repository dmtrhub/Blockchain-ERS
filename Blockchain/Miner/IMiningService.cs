using Ers;

namespace Ers
{
    public interface IMiningService
    {
        void MineBlock(IBlock block, IMiner miner);
    }
}