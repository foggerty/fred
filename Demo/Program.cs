using Fred;
using Demo.APIs.Wombat;
using Demo.APIs.Weather;
using Fred.Abstractions.PublicFacing;

var config = Bootstrap.ReadConfiguration();  // or provide your own

var server = Bootstrap

    .NewServer(config)

    .UseSelfSignedCertificate()

    .RegisterEndpoint<WeatherApi, WeatherEndpoint, int>()
    .RegisterEndpoint<WombatApi, WombatEndpoint, string>()
    .RegisterEndpoint<WombatApi, WeatherEndpoint, int>()

    .SetupServices(ServicesSetup)

    .Done();

server.StartApis(TimeSpan.FromSeconds(30));


void ServicesSetup(IServiceLocatorSetup setup)
{
    var fred = config.Database.ConnectionString;
    
    setup.RegisterSingleton<string, string>(x => fred);
}