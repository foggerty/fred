using Demo.APIs.Weather;
using Demo.APIs.Wombat;
using Demo.Services;
using Fred;
using Fred.Abstractions.PublicFacing.Services;

const string _globalName = "Global";
const string _apiName = "Api Specific";

static void GlobalServices(IServicesSetup setup)
{
    var service = new WunderService(_globalName);
    
    setup.RegisterSingleton<IWunderService>(service);
}

static void ApiServices(IServicesSetup setup)
{
    var service = new WunderService(_apiName);
    
    setup.RegisterSingleton<IWunderService, WeatherApi>(service);
}

static void TestServices(IServicesSetup setup)
{
    if (setup.Get<IWunderService>().WunderName != _globalName)
        throw new Exception("Oh noes!");
    
    if (setup.Get<IWunderService, WeatherApi>().WunderName != _apiName)
        throw new Exception("Oh noes!");
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
    .ConfigureServices(GlobalServices)
    .ConfigureServices(ApiServices)
    .ConfigureServices(TestServices)
    .Done();

server.StartApis(TimeSpan.FromSeconds(30));
