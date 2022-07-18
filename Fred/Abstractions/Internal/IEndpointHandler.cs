using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

// ToDo - what about this interface suggests HTTP?
// Come up with a better model/pipeline abstraction.
public interface IEndpointHandler
{
    void RegisterHandler<Q>(string root, string path, Func<Q, IAnswer> handler);

    void Start(TimeSpan timeout, CancellationToken token);

    void Stop(TimeSpan timeout, CancellationToken token);
}