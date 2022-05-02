using Fred.Abstractions.PublicFacing;

namespace Fred.Implimentations.Internal;

internal class DatabaseConfiguration : IDatabaseConfiguration
{
    public string ConnectionString => "";
}

internal class Config : IConfig
{
    public IDatabaseConfiguration? Database { get; internal set; }
}