using Fred.Abstractions.PublicFacing;

namespace Demo.APIs.Weather;

public class WeatherConfig : IApiConfig<WeatherApi>
{
    public string Units = "Celsius";
}