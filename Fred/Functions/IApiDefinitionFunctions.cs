using Fred.Abstractions.PublicFacing;

namespace Fred.Functions;

public static class IApiDefinitionFunctions
{
    public static string PathTo(this IApiDefinition definition, IApiEndpoint endpoint)
    {
        var root = new Uri(definition.Root);
        var path = new Uri(root, endpoint.Path);

        return path.ToString();        
    }
}