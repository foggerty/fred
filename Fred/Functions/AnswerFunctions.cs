using Fred.Abstractions.PublicFacing;
using Fred.Implimentations;

namespace Fred.Functions;

public static class AnswerFunctions
{
    public static IAnswer<T> ToAnswer<T>(this T response, HttpStatusCode statusCode)
    {
        return new Answer<T>(response, statusCode);
    }
}