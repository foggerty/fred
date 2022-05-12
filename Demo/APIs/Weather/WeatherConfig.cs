using System.Runtime.Serialization;
using Fred.Abstractions.PublicFacing;

namespace Demo.APIs.Weather;

public class WeatherConfig : IApiConfig<WeatherApi>
{
    public int MeasuremeantUnits { get; set; }
}