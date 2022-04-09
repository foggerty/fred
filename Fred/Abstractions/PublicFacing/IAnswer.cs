using System.Net;

namespace Fred.Abstractions.PublicFacing
{
    public interface IAnswer
    {

    }
    
    public interface IAnswer<T> : IAnswer
    {
        T Response { get; }

        HttpStatusCode StatusCode { get; }

        ICollection<(string, string)> ExtraHeaders { get; }

        bool Cachable { get; set; }

        TimeSpan CacheLimit { get; set; }
    }    
}