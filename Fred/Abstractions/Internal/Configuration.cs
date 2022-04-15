using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

public class DatabaseConfiguration : IDatabaseConfiguration
{
    public string ConnectionString => "";
}

public class Configuration : IConfiguration
{
    public IDatabaseConfiguration Database => new DatabaseConfiguration();
}