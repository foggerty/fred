using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;

namespace Fred.Implimentations.Internal;

internal class ApiMap : IApiMap
{    
    public void AddEndpoint<APIDEF, ENDPOINT, Q>()
        where APIDEF : IApiDefinition, new()
        where ENDPOINT : IApiEndpointHandler<Q>, new()
    {        
    }

    public IEnumerable<Type> QuestionTypes(string root)
    {
        throw new NotImplementedException();
    }

    public IApiEndpoint GetHandler(string root, Type question)
    {
        throw new NotImplementedException();
    }
}
