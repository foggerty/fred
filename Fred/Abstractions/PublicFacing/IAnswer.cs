using System.Net;

namespace Fred.Abstractions.PublicFacing
{
    public interface IAnswer
    {
        HttpStatusCode StatusCode { get; }

        internal string AsJSON();
    }
    
    public interface IAnswer<T> : IAnswer
    {
        T Response { get; }        
    }
}