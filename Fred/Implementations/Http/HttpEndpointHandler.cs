using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;

namespace Fred.Implementations.Http;

internal class HttpEndpointHandler : IEndpointHandler, IHttpApplication<IFeatureCollection>
{
    private readonly byte[] helloWorldBytes = Encoding.UTF8.GetBytes("hello world");

    public void RegisterHandler<Q>(string root, string path, Func<Q, IAnswer> handler)
    {
        throw new NotImplementedException();
    }

    public void Start(TimeSpan timeout, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public void Stop(TimeSpan timeout, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public IFeatureCollection CreateContext(IFeatureCollection contextFeatures)
    {
        return contextFeatures;
    }

    public void DisposeContext(IFeatureCollection context, Exception? exception)
    {
    }

    public async Task ProcessRequestAsync(IFeatureCollection features)
    {
        var request      = (IHttpRequestFeature)features[typeof(IHttpRequestFeature)];
        var response     = (IHttpResponseFeature)features[typeof(IHttpResponseFeature)];
        var responseBody = (IHttpResponseBodyFeature)features[typeof(IHttpResponseBodyFeature)];
        ////var connection = (IHttpConnectionFeature)features[typeof(IHttpConnectionFeature)];
        response.Headers.ContentLength = helloWorldBytes.Length;
        response.Headers.Add("Content-Type", "text/plain");
        await responseBody.Stream.WriteAsync(helloWorldBytes, 0, helloWorldBytes.Length);
    }
}