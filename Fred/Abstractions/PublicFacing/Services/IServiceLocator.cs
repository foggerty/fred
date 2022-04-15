namespace Fred.Abstractions.PublicFacing.Services;

public partial interface IServiceLocator
{
    public T Get<T>();    
}

