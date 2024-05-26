namespace Ers
{
    public interface IClient
    {
        int Id { get; }

        void SendData(ISmartContract smartContract, string data);
    }
}