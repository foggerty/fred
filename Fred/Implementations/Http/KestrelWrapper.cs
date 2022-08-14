using Fred.Implimentations.Http;

namespace Fred.Implementations.Http;

internal class KestrelWrapper
{
    public KestrelWrapper()
    {
        var loggerFactory          = new NoOpLoggerFactory();
        var serverOptions          = new ConfigureKestrelServerOptions();
        var transportOptions       = new ConfigureSocketTransportOptions();
        var socketTransportFactory = new SocketTransportFactory(transportOptions, loggerFactory);
        var kestrelServer          = new KestrelServer(serverOptions, socketTransportFactory, loggerFactory);

        kestrelServer.Options.ListenLocalhost(8080);

        kestrelServer.StartAsync(new HttpEndpointHandler(), CancellationToken.None).GetAwaiter().GetResult();

        Console.WriteLine("ENTER to quit.....");
        Console.ReadLine();
        Console.WriteLine("shutting down");
        kestrelServer.StopAsync(CancellationToken.None).GetAwaiter().GetResult();
    }
}