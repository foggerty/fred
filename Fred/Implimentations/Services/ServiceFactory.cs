using System.Net.Http.Headers;
using Fred.Abstractions.Internal.Services;
using Fred.Exceptions;
using Fred.Functions;

namespace Fred.Implimentations.Services;

internal class ServiceFactory : IServiceFactory
{    
    private readonly Dictionary<Type, Type> _toCreate = new();
    private readonly ServiceContainer _singletons = new();

    private const string PleaseRegister = "Before you ask for '{0}', first register it.";
    private const string CannotRegisterANull = "Cannot register a null instance for interface '{0}' (or ANY interface, for that matter).  I mean, seriously...";
    private const string DoNotRegisterTwice = "You've tried to register {0} twice.  Please refrain from doing so again.";
        
    public I Get<I>()
    {
        return (I)Get(typeof(I));
    }
    
    public void RegisterSingleton<I>(I instance)
    {
        typeof(I).MustBeInterface();

        if(instance == null)
            throw new DeveloperException(CannotRegisterANull, nameof(I));

        TestNotAlreadyRegistered<I>();

        _singletons.AddService(typeof(I), instance);
    }

    public void RegisterSingleton<I, T>()
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(I));
        typeof(T).MustHavePublicConstructor();

        TestNotAlreadyRegistered<I>();
        
        _toCreate[typeof(I)] = typeof(T);
    }

    private object Get(Type i)
    {
        i.MustBeInterface();

        var service = FromSingletons(i) ?? NewInstance(i);

        if(service == null)
        {
            throw new DeveloperException(PleaseRegister, i.Name);
        }                              

        return service;
    }
    
    private void TestNotAlreadyRegistered<I>()
    {
        if(_singletons.GetService(typeof(I)) != null)
            throw new DeveloperException(DoNotRegisterTwice, typeof(I));
        
        if(_toCreate.ContainsKey(typeof(I)))
            throw new DeveloperException(DoNotRegisterTwice, nameof(I));
    }
    
    private I? FromSingletons<I>()
    {
        var result = _singletons.GetService(typeof(I));

        return result == null
            ? default
            : (I)result;
    }

    private object? FromSingletons(Type i)
    {
        return _singletons.GetService(i);
    }

    private I NewInstance<I>()
    {
        return (I)NewInstance(typeof(I));
    }

    // Yes, niave implementation for now, and will break with circular dependencies.
    private object NewInstance(Type i)
    {       
        var typeToCreate = _toCreate[i];

        var constructor = typeToCreate?.DefaultConstructor();

        if(constructor == null)
            throw new DeveloperException("It's all gone horribly wrong, this exception shouldn't even be seen :-(");

        object instance;

        if(constructor.GetParameters().Length == 0)
        {
            instance = constructor.Invoke(null);
        }
        else
        {
            var parameters = constructor
                .GetParameters();

            var instances = new List<object>();
                        
            foreach(var parameter in parameters)
            {
                instances.Add(Get(parameter.ParameterType));
            }

            instance = constructor.Invoke(instances.ToArray());
        }

        _singletons.AddService(i, instance);

        return instance;
    }    
}
