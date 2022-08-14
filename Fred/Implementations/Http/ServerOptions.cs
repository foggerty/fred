namespace Fred.Implimentations.Http;

public class ConfigureKestrelServerOptions : IOptions<KestrelServerOptions>
{
    public ConfigureKestrelServerOptions()
    {
        Value = new KestrelServerOptions();
    }

    public KestrelServerOptions Value { get; }
}