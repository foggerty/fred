using Fred.Abstractions.PublicFacing;

namespace Fred.Implementations;

internal class Answer<T> : IAnswer<T>
{
    private static readonly JsonSerializerOptions _jso = new JsonSerializerOptions
    {
        AllowTrailingCommas = true,
        WriteIndented       = true
    };
    
    public Answer(T response)
    {
        Response = response;
    }

    public T Response { get; }

    public string AsJSON()
    {
        return JsonSerializer.Serialize(Response, _jso);
    }

    public Type ResponseType()
    {
        return typeof(T);
    }
}