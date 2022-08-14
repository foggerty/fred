namespace Fred.Implementations.Http;

public sealed class ConfigureSocketTransportOptions : IOptions<SocketTransportOptions>
{
    public ConfigureSocketTransportOptions()
    {
        Value = new SocketTransportOptions();
    }

    public SocketTransportOptions Value { get; }
}