using System.Net;
using System.Text.Json;
using Fred.Abstractions.PublicFacing;

namespace Fred.Implimentations.Internal;

internal class Answer<T> : IAnswer
{
    public Answer(T response, HttpStatusCode statusCode)
    {
        Response = response;
        StatusCode = statusCode;
    }

    public T Response { get; }

    public HttpStatusCode StatusCode { get; }

    public string AsJSON()
    {
        return JsonSerializer.Serialize(Response);
    }
}