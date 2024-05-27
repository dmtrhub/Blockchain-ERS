namespace Ers
{
    public interface INotificationService
    {
        void NotifyMiners(IMiner thisMiner, IBlock block);
    }
}