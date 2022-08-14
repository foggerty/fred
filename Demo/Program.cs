using Demo.APIs.Weather;
using Demo.APIs.Wombat;
using Demo.Services;
using Fred;
using Fred.Abstractions.PublicFacing.Services;

var config = LoadConfig.FromDefault();

var server = Bootstrap
             .NewServer(config)
             .UseSelfSignedCertificate()
             .RegisterConfig<WeatherApi, WeatherConfig>()
             .RegisterEndpoint<WeatherApi, WeatherEndpoint, int>()
             .RegisterEndpoint<WombatApi, WombatEndpoint, string>()
             .RegisterEndpoint<WombatApi, WeatherEndpoint, int>()
             .AddServices(ServicesSetup)
             .Done();

server.StartApis(TimeSpan.FromSeconds(30));


static void ServicesSetup(IServicesSetup setup)
{
    var weatherConfig = setup.Get<WeatherConfig>();
    
    Console.WriteLine(weatherConfig.Units);
    
    setup.RegisterSingleton(() => new WunderService());
}