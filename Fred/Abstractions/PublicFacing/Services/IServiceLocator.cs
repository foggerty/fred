namespace Fred.Abstractions.Internal.Services;

public interface IServiceLocator
{
    public I Get<I>();
}

