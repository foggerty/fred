namespace Fred.Abstractions.PublicFacing;

public interface IApi
{
    public IApiDefinition Definition { get; }

    public IEnumerable<IApiEndpoint> Endpoints { get; }
}