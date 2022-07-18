namespace Fred.Abstractions.PublicFacing;

public interface IServerController
{
    void StartApis(TimeSpan timeout);

    void StopApis(TimeSpan timeout);

    IEnumerable<IApiDefinition> AllApis();

    // Running stats?
}