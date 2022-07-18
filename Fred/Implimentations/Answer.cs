using System.Text;
using System.Diagnostics.CodeAnalysis;
using Fred.Abstractions.PublicFacing;

namespace Fred.Implimentations;

internal class Answer<T> : IAnswer<T>
{
    public Answer(T response)
    {
        Response = response;
    }

    public T Response { get; }

    public string AsJSON()
    {
        var jso = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true,            
        };
        
        return JsonSerializer.Serialize(Response, jso);
    }

    public Type ResponseType()
    {
        return typeof(T);
    }
}