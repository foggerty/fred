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

    .SetupServices(ServicesSetup)

    .Done();

server.StartApis(TimeSpan.FromSeconds(30));


void ServicesSetup(IServiceLocatorSetup setup, IConfiguration config)
{
    var fred = config.Database?.ConnectionString;

    if(fred == null)
        return;
    
    setup.RegisterSingleton<string>(() => fred);
}