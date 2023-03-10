using Fred.Abstractions.Internal.Services;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Functions;

namespace Fred.Implementations.Services;

internal class ServiceFactory : IServiceFactory
{
    private const string CannotRegisterANull = "Cannot register a null instance for interface '{0}' (or ANY interface, for that matter).  I mean, seriously...";
    private const string DoNotRegisterTwice = "You've tried to register {0} twice.  Please refrain from doing so again.";
    private const string YouNeverRegisteredThisForThat = "You never once - ONCE! - registered {0} so that it may be used when creating {1}.  You.  Monster.";
    private const string YouNeverRegistered = "You have never registered or indicated in any manner how I might procure a {0}, and yet you now ask me for one?  For shame.";
    private const string YouBrokeEverything = "All I asked you to do, was to provide me with a factory function to create a {0} as needed that DIDN'T EXPLODE when I called it.";
    
    private readonly object _lock = new(); // ToDo - Rethink the locking strategy when sober.  Ahem.
    private readonly IServiceContainer _singletons = new ServiceContainer();
    private readonly Dictionary<Type, Func<object>> _toCreate = new();

    public I Get<I>()
        where I : IFredService
    {
        var instance = Get(typeof(I));

        if (instance == null)
            throw new DeveloperException(YouNeverRegistered, typeof(I).Name);

        return (I)instance;
    }

    public void RegisterSingleton<I>(I instance)
        where I : IFredService
    {
        typeof(I).MustBeInterface();
        typeof(I).MustBeAllowedService();

        if (instance == null)
            throw new DeveloperException(CannotRegisterANull, nameof(I));

        TestNotAlreadyRegistered<I>();

        _singletons.AddService(typeof(I), instance);
    }

    public void RegisterSingleton<I, T>()
        where I : IFredService
    {
        typeof(I).MustBeInterface();
        typeof(I).MustBeAllowedService();
        typeof(T).MustImplement(typeof(I));
        typeof(T).MustHaveDIFriendlyConstructor();

        TestNotAlreadyRegistered<I>();

        _toCreate[typeof(I)] = () => NewInstance<T>();
    }

    public void RegisterSingleton<I>(Func<I> creator)
        where I : IFredService
    {
        typeof(I).MustBeInterface();
        typeof(I).MustBeAllowedService();

        TestNotAlreadyRegistered<I>();

        _toCreate[typeof(I)] = () => creator();
    }

    private object Get(Type i)
    {
        i.MustBeInterface();

        var result = FromSingletons(i);

        if (result != null)
            return result;

        try
        {
            if (_toCreate.ContainsKey(i))
                result = _toCreate[i]();
        }
        catch (Exception ex)
        {
            throw new DeveloperException(ex, YouBrokeEverything, i.Name);
        }

        if (result == null)
            throw new DeveloperException(YouNeverRegistered, i.Name);

        _singletons.AddService(i, result);

        return result;
    }

    private void TestNotAlreadyRegistered<I>()
    {
        if (_singletons.GetService(typeof(I)) != null)
            throw new DeveloperException(DoNotRegisterTwice, typeof(I));

        if (_toCreate.ContainsKey(typeof(I)))
            throw new DeveloperException(DoNotRegisterTwice, nameof(I));
    }

    private object? FromSingletons(Type i)
    {
        return _singletons.GetService(i);
    }

    private T NewInstance<T>()
    {
        return (T)NewInstance(typeof(T));
    }

    private object NewInstance(Type t)
    {
        var constructor = t.DefaultConstructorForDi();

        object instance;

        if (constructor.GetParameters().Length == 0)
        {
            instance = constructor.Invoke(null);
        }
        else
        {
            var parameters = constructor
                             .GetParameters()
                             .OrderBy(p => p.Position);

            var parameterInstances = new List<object>();

            foreach (var parameter in parameters)
            {
                var parameterInstance = Get(parameter.ParameterType);

                if (parameterInstance == null)
                    throw new DeveloperException(YouNeverRegisteredThisForThat, parameter.ParameterType.Name, t.Name);

                parameterInstances.Add(parameterInstance);
            }

            instance = constructor.Invoke(parameterInstances.ToArray());
        }

        lock (_lock)
        {
            _singletons.AddService(t, instance);
        }

        if (instance == null)
            throw new Exception();

        return instance;
    }
}