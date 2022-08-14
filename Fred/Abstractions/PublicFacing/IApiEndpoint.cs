namespace Fred.Abstractions.PublicFacing;

public interface IApiEndpointHandler
{
    public string Path { get; }
}

public interface IApiEndpointHandler<Q> : IApiEndpointHandler
{
    public Func<Q, IAnswer> Handler { get; }
}