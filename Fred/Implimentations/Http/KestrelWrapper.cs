namespace Fred.Implimentations.Http;

internal class KestrelWrapper
{
    public KestrelWrapper()
    {
        var loggerFactory = new NoOpLoggerFactory();
        var csko = new  ConfigureKestrelServerOptions();
        var csto = new ConfigureSocketTransportOptions();
        var stf = new SocketTransportFactory(csto, loggerFactory);
        var kestrelServer = new KestrelServer(csko, stf, loggerFactory);

        kestrelServer.Options.ListenLocalhost(8080);
        
        kestrelServer.StartAsync(new HttpEndpointHandler(), CancellationToken.None).GetAwaiter().GetResult();

        Console.WriteLine("ENTER to quit.....");
        Console.ReadLine();
        Console.WriteLine("shutting down");
        kestrelServer.StopAsync(CancellationToken.None).GetAwaiter().GetResult();
    }
}