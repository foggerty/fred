using System.Net;
using Fred.Abstractions.PublicFacing;
using Fred.Implimentations.PublicFacing;

namespace Fred.Functions
{
    public static class AnswerFunctions
    {
        public static IAnswer ToAnswer<T>(this T response, HttpStatusCode statusCode)
        {
            return new Answer<T>(response, statusCode);
        }
    }
}