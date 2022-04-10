namespace Fred.Abstractions.PublicFacing.Services
{
    public interface IObjectStore : IService
    {
        void Add(object key, object value);

        T Get<T>(object key);
    }
}