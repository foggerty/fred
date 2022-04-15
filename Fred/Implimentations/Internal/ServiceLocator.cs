using System.ComponentModel.Design;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Functions;

namespace Fred.Implimentations.Internal;

internal class ServiceLocator : IServiceLocator, IServiceLocatorSetup
{
    private readonly ServiceContainer _singletonServices = new();
    private readonly ServiceContainer _transientServices = new();
    
    public T Get<T>()
    {
        throw new NotImplementedException();
    }

    public T Get<T, API>() where API : IApiDefinition
    {
        throw new NotImplementedException();
    }

    public void RegisterSingleton<I, T>()
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterSingleton<I, T, API>()
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterSingleton<I, T>(Func<IServiceLocator, T> create)
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterSingleton<I, T, API>(Func<IServiceLocator, T> create)
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterSingleton<I>(Func<IServiceLocator, I> create)
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterSingleton<I, API>(Func<IServiceLocator, I> create) where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterTransient<I, T>()
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterTransient<I, T, API>()
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterTransient<I, T>(Func<IServiceLocator, T> create)
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterTransient<I, T, API>(Func<IServiceLocator, T> create)
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }   
}