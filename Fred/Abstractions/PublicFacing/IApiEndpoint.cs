namespace Fred.Abstractions.PublicFacing;

public interface IApiEndpoint
{
    public string Path { get; }
}

public interface IApiEndpointHandler<Q> : IApiEndpoint
{               
    public Func<Q, IAnswer> Handler { get; }
}