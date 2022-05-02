using System.Runtime.InteropServices;

namespace Fred.Abstractions.PublicFacing;

public interface IApiDefinition
{
    public string Name { get; }

    public string Description { get; }

    public Version Version { get; }

    public string Root { get; }

    public Type? ConfigurationType();
}