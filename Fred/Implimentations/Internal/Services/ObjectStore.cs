using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;

namespace Fred.Implimentations.Internal.Services;

public class ObjectStore : IObjectStore
{
    private readonly Dictionary<object, object> _store = new();
      
    public void Add(object key, object value)
    {
        _store.Add(key, value);
    }

    public T Get<T>(object key)
    {
        if(!_store.ContainsKey(key))
            throw new DeveloperException($"You asked for a {key.GetType().FullName} before giving, how heartless of you.");

        var test = (T)_store[key];

        if(test == null)
            throw new DeveloperException($"");

        return test;
    }
}