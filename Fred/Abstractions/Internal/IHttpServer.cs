using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

public interface IHttpServer
{
    void RegisterHandlers();

    void Start(TimeSpan timeout);

    void Stop(TimeSpan timeout);
}