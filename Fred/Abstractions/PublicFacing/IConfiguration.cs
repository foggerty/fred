namespace Fred.Abstractions.PublicFacing;

public interface IDatabaseConfiguration
{
    public string ConnectionString { get; }
}

public interface IConfiguration
{
    public IDatabaseConfiguration? Database { get; }
}