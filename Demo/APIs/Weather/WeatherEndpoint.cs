using System.Net;
using Fred.Abstractions.PublicFacing;
using Fred.Functions;

namespace Demo.APIs.Weather;

public class WeatherEndpoint : IApiEndpointHandler<int>
{
    public string Path => "dostuff";

    public Func<int, IAnswer> Handler => x =>
        x.ToString().ToAnswer(HttpStatusCode.OK);  // point out that this is why, like all abstractions, this one leaks slightly.
}

