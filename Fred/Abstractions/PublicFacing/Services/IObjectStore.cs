using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.PublicFacing.Services
{
    public interface IObjectStore : IService
    {
        void Add<T>(object key, T value);

        T Get<T>(object key);
    }
}