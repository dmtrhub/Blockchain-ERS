namespace Ers
{
    public interface IBlockchain
    {
        List<IBlock> Chain { get; }
        int Digits { get; set; }

        void AddBlock(IBlock newBlock);
        IBlock GetLatestBlock();
    }
}