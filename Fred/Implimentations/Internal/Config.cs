using Fred.Abstractions.PublicFacing;

namespace Fred.Implimentations.Internal;

internal class Config : IConfig
{
    public object ApiConfigFor<T>() where T : IApiDefinition
    {
        throw new NotImplementedException();
    }

    public void ReadFromFile(string fileName)
    {
        throw new NotImplementedException();
    }
}