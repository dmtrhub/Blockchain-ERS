
namespace Ers
{
    public interface IBlock
    {
        int ClientId { get; set; }
        string Data { get; set; }
        int Digits { get; set; }
        string Hash { get; set; }
        int Num { get; set; }
        string PreviousHash { get; set; }
        DateTime Timestamp { get; set; }

        string ToString();
    }
}