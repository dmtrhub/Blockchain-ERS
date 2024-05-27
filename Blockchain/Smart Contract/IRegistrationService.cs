namespace Ers
{
    public interface IRegistrationService
    {
        List<IClient> registeredClients { get; }
        List<IMiner> registeredMiners { get; }

        void RegisterClient(IClient client);
        void RegisterMiner(IMiner miner);
    }
}