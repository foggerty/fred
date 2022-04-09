using Fred.Abstractions.PublicFacing;

namespace Demo.APIs.Weather
{
    public class WeatherApi : IApiDefinition
    {
        public string Name => "Weather API";

        public string Description => "For all your weather needs!";

        public Version Version => new(1, 0, 0, 0);

        public string Root => "weather";
    }
}