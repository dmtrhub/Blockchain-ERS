namespace Ers
{
    public interface IMiner
    {
        double BitcoinBalance { get; }
        string Id { get; }
        List<IBlock> LocalBlockchain { get; set; }

        void ConfirmBlock(IBlock block);
        void MineBlock(IBlock block);
        List<IMiner> RegisterWithSmartContract(ISmartContract smartContract);
        string ToString();
        bool ValidateBlock(IBlock block);
    }
}