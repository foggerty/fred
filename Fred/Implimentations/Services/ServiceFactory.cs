using Fred.Abstractions.Internal.Services;
using Fred.Exceptions;
using Fred.Functions;

namespace Fred.Implimentations.Services;

internal class ServiceFactory : IServiceFactory
{    
    private readonly object _lock = new();
    
    private readonly Dictionary<Type, Type> _toCreate = new();
    private readonly IServiceContainer _singletons = new ServiceContainer();

    private const string CannotRegisterANull = "Cannot register a null instance for interface '{0}' (or ANY interface, for that matter).  I mean, seriously...";
    private const string DoNotRegisterTwice = "You've tried to register {0} twice.  Please refrain from doing so again.";
    private const string YouNeverRegisteredThisForThat = "You never once - ONCE! - registered {0} so that it may be used when creating {1}.  You.  Monster.";
        
    public I? Get<I>()
    {
        lock(_lock)
        {
            var instance = Get(typeof(I));

            return instance == null
                ? default
                : (I)instance;
        }
    }
    
    public void RegisterSingleton<I>(I instance)
    {
        typeof(I).MustBeInterface();

        if(instance == null)
            throw new DeveloperException(CannotRegisterANull, nameof(I));

        lock(_lock)
        {
            TestNotAlreadyRegistered<I>();

            _singletons.AddService(typeof(I), instance);
        }
    }

    public void RegisterSingleton<I, T>()
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(I));
        typeof(T).MustHaveDiConstructor();

        lock(_lock)
        {
            TestNotAlreadyRegistered<I>();
            
            _toCreate[typeof(I)] = typeof(T);
        }
    }

    private object? Get(Type i)
    {
        i.MustBeInterface();

        return FromSingletons(i) ?? NewInstance(i);        
    }
    
    private void TestNotAlreadyRegistered<I>()
    {
        if(_singletons.GetService(typeof(I)) != null)
            throw new DeveloperException(DoNotRegisterTwice, typeof(I));
        
        if(_toCreate.ContainsKey(typeof(I)))
            throw new DeveloperException(DoNotRegisterTwice, nameof(I));
    }
    
    private object? FromSingletons(Type i)
    {
        return _singletons.GetService(i);
    }

    private I? NewInstance<I>()
    {
        var instance = NewInstance(typeof(I));

        return instance == null
            ? default
            : (I)instance;
    }

    private object? NewInstance(Type i)
    {       
        if(!_toCreate.ContainsKey(i))
            return default;
        
        var typeToCreate = _toCreate[i];
        var constructor = typeToCreate.DefaultConstructorForDi();

        object instance;

        if(constructor.GetParameters().Length == 0)
        {
            instance = constructor.Invoke(null);
        }
        else
        {
            var parameters = constructor
                .GetParameters()
                .OrderBy(p => p.Position);

            var parameterInstances = new List<object>();
                        
            foreach(var parameter in parameters)
            {
                var parameterInstance = Get(parameter.ParameterType);

                if(parameterInstance == null)
                    throw new DeveloperException(YouNeverRegisteredThisForThat, parameter.ParameterType.Name, i.Name);

                parameterInstances.Add(parameterInstance);
            }

            instance = constructor.Invoke(parameterInstances.ToArray());
        }

        _singletons.AddService(i, instance);

        return instance;
    }    
}