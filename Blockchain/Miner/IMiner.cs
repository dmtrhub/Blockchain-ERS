namespace Ers
{
    public interface IMiner : IBlockConfirmation, IMiningService, IBlockValidator
    {
        List<IBlock> LocalChain { get; set; }
        string Id { get; set; }

        double BitcoinBalance { get; set; }
    }
}