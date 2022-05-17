namespace Fred.Abstractions.PublicFacing;

public interface IAnswer
{
    HttpStatusCode StatusCode { get; }    

    public string AsJSON();

    public Type ResponseType();
}

public interface IAnswer<T> : IAnswer
{
    T Response { get; }    
}