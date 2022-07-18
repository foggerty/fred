namespace Fred.Implimentations.Http;

public sealed class ConfigureSocketTransportOptions : IOptions<SocketTransportOptions>
{
    public ConfigureSocketTransportOptions()
    {
        this.Value = new SocketTransportOptions()
        {
        };
    }

    public SocketTransportOptions Value { get; }
}