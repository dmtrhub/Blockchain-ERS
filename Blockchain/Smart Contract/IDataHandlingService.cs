 namespace Ers
{
    public interface IDataHandlingService
    {
        void ReceiveData(IClient client, string data);
    }
}