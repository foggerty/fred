using System.Runtime.Serialization;

namespace Demo.APIs.Weather
{
    // Config is a type.....?
    // It's a mutable type.  It does'n get an abstraction.
    public class WeatherConfig
    {
        public int MeasuremeantUnits { get; set; }
    }
}