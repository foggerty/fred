using System.Net.Http.Headers;
using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

internal class DatabaseConfiguration : IDatabaseConfiguration
{
    public string ConnectionString => "";
}

internal class Configuration : IConfiguration
{
    public IDatabaseConfiguration? Database { get; internal set; }
}