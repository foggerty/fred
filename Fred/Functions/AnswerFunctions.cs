using System.Net;
using Fred.Abstractions.PublicFacing;
using Fred.Implimentations.Internal;

namespace Fred.Functions;

public static class AnswerFunctions
{
    public static IAnswer<T> ToAnswer<T>(this T response, HttpStatusCode statusCode)
    {
        return new Answer<T>(response, statusCode);
    }
}