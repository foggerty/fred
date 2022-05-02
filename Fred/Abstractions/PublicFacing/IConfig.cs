namespace Fred.Abstractions.PublicFacing;

public interface IDatabaseConfiguration
{
    public string ConnectionString { get; }
}

public interface IConfig
{
    public IDatabaseConfiguration? Database { get; }
}