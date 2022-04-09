using System.Net;

using Fred.Abstractions.PublicFacing;

namespace Fred.Implimentations.PublicFacing
{
    internal class Answer<T> : IAnswer<T>
    {
        public Answer(T response, HttpStatusCode statusCode)
        {
            Response = response;
            StatusCode = statusCode;
            ExtraHeaders = new List<(string, string)>();
        }

        public T Response { get; }

        public HttpStatusCode StatusCode { get; }

        public ICollection<(string, string)> ExtraHeaders { get; }

        public bool Cachable { get; set; }

        public TimeSpan CacheLimit { get; set; }
    }
}