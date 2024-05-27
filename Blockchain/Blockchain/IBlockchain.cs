using Ers;

namespace Ers
{
    public interface IBlockchain
    {
        void AddBlock(IBlock newBlock);
        List<IBlock> GetChain();
        IBlock GetLatestBlock();
    }
}