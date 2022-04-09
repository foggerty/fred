namespace Fred.Abstractions.PublicFacing
{
    public interface IServer
    {
        void StartApis(TimeSpan timeout);

        void StopApis(TimeSpan timeout);
    }
}