namespace Fred.Abstractions.PublicFacing
{
    public interface ITicket
    {
        int TicketNumber { get; set; }

        DateTime Expiry { get; set; }

        
    }
}