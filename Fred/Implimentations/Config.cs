using Fred.Abstractions.PublicFacing;

namespace Fred.Implimentations;

internal class Config : IConfig
{
    public object ApiConfigFor<I>()
        where I : IApiDefinition
    {
        throw new NotImplementedException();
    }

    internal void ReadFromFile(string fileName)
    {
        throw new NotImplementedException();
    }
}