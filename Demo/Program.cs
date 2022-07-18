using Fred;
using Demo.APIs.Wombat;
using Demo.APIs.Weather;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;

var server = Bootstrap

    .NewServer()

    .UseSelfSignedCertificate()

    .AddServices(ServicesSetup)

    .RegisterEndpoint<WeatherApi, WeatherEndpoint, int>()
    .RegisterEndpoint<WombatApi, WombatEndpoint, string>()
    .RegisterEndpoint<WombatApi, WeatherEndpoint, int>()    

    .Done();

server.StartApis(TimeSpan.FromSeconds(30));




static void ServicesSetup(IApiServicesSetup setup, IConfig config)
{
       
}