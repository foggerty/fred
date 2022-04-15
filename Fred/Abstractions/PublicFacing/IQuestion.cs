namespace Fred.Abstractions.PublicFacing;

public interface IQuestion<T>
{
    int TicketNumber { get; set; }

    T Request { get; set; }
}