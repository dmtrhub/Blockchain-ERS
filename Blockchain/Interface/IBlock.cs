namespace Ers
{
    public interface IBlock
    {
        int ClientId { get; set; }
        string Data { get; set; }
        string Hash { get; set; }
        int Num { get; set; }
        string PreviousHash { get; set; }
        DateTime Timestamp { get; set; }

        string CalculateHash();
        string MineBlock(int digits);
        string ToString();
    }
}