using Fred;
using Demo.APIs.Wombat;
using Demo.APIs.Weather;
using Fred.Abstractions.PublicFacing.Services;

var config = Bootstrap.ReadConfiguration();  // or provide your own

var server = Bootstrap

    .NewServer(config)

    .UseSelfSignedCertificate()

    .RegisterEndpoint<WeatherApi, WeatherEndpoint, int>()
    .RegisterEndpoint<WombatApi, WombatEndpoint, string>()
    .RegisterEndpoint<WombatApi, WeatherEndpoint, int>()

    .RegisterSingleton<IObjectStore>(() => null)
    .RegisterTransient<IObjectStore, WeatherApi>(() => null)

    .Done();

server.StartApis(TimeSpan.FromSeconds(30));