namespace Ers
{
    public interface IClient
    {
        int Id { get; }

        void SendData(string data);
    }
}