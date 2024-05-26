namespace Ers
{
    public interface ISmartContract
    {
        List<IClient> registeredClients { get; }
        List<IMiner> registeredMiners { get; }

        void AssignTask(IBlock block);
        void NotifyMiners(IMiner thisMiner, IBlock block);
        void ReceiveData(IClient client, string data);
        void RegisterClient(IClient client);
        void RegisterMiner(IMiner miner);
    }
}