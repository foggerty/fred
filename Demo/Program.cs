using Fred;
using Demo.APIs.Wombat;
using Demo.APIs.Weather;
using Fred.Abstractions.PublicFacing;

var server = Bootstrap

    .NewServer()

    .UseSelfSignedCertificate()

    .RegisterEndpoint<WeatherApi, WeatherEndpoint, int>()
    .RegisterEndpoint<WombatApi, WombatEndpoint, string>()
    .RegisterEndpoint<WombatApi, WeatherEndpoint, int>()

    .AddServices(ServicesSetup)

    .Done();

server.StartApis(TimeSpan.FromSeconds(30));




static void ServicesSetup(IServiceLocatorSetup setup, IConfig config)
{    
    
}