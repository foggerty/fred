namespace Fred.Abstractions.PublicFacing.Services;

public interface IServiceLocator
{
    public T Get<T>();    

    public T Get<T, API>()
        where API : IApiDefinition;
}