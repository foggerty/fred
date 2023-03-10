using Demo.APIs.Weather;
using Demo.APIs.Wombat;
using Demo.Services;
using Fred;
using Fred.Abstractions.PublicFacing.Services;

static void ServicesSetup(IServicesSetup setup)
{
    var weatherConfig = setup.Get<WeatherConfig>();
    
    Console.WriteLine(weatherConfig.Units);
    
    setup.RegisterSingleton(() => new WunderService());
}

var config = LoadConfig.FromDefault();

var server = Bootstrap
    .NewServer(config)
    .UseSelfSignedCertificate()
    .RegisterConfig<WeatherApi, WeatherConfig>()
    .AddHandler<WeatherApi, WeatherEndpoint, int>()
    .AddHandler<WombatApi, WombatEndpoint, string>()
    .AddHandler<WombatApi, WeatherEndpoint, int>()
    .AllowAccessToFileSystem()
    .ConfigureServices(ServicesSetup)
    .Done();

server.StartApis(TimeSpan.FromSeconds(30));
