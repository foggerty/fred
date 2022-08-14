using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

public interface IEndpointHandler
{
    void RegisterHandler<Q>(string root, string path, Func<Q, IAnswer> handler);

    void Start(TimeSpan timeout, CancellationToken token);

    void Stop(TimeSpan timeout, CancellationToken token);
}