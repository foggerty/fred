using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal
{
    internal interface ISession
    {
        int Create(TimeSpan timeout);
        
        ITicket Redeem(int ticketNumber);

        T GetService<T>()
            where T : IService;
    }
}