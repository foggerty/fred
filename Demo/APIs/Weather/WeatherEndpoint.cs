using Fred.Abstractions.PublicFacing;
using Fred.Functions;

namespace Demo.APIs.Weather;

public class WeatherEndpoint : IApiEndpointHandler<int>
{
    private readonly WeatherConfig _config;
    
    public WeatherEndpoint(WeatherConfig config)
    {
        _config = config;
    }
    
    public string Path => "dostuff";

    public Func<int, IAnswer> Handler => x =>
        (_config.MeasuremeantUnits * x)
        .ToString()
        .ToAnswer();
}

