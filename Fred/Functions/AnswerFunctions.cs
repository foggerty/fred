using Fred.Abstractions.PublicFacing;
using Fred.Implementations;

namespace Fred.Functions;

public static class AnswerFunctions
{
    public static IAnswer<T> ToAnswer<T>(this T response)
    {
        return new Answer<T>(response);
    }
}