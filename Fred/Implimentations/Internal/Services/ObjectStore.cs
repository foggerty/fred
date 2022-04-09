using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Implimentations.Internal.Services;

public class ObjectStore : IObjectStore
{
    public void Add<T>(object key, T value)
    {
        throw new NotImplementedException();
    }

    public T Get<T>(object key)
    {
        throw new NotImplementedException();
    }
}