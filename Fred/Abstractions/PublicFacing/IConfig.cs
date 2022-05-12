using Microsoft.AspNetCore.Components.Web;

namespace Fred.Abstractions.PublicFacing;

public interface IConfig
{
    // There should be a class in the config collection that's keyed off of the 
    public object ApiConfigFor<T>()
        where T : IApiDefinition;
    
    public void ReadFromFile(string fileName);
}