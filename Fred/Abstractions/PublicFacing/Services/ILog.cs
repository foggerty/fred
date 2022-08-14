namespace Fred.Abstractions.PublicFacing.Services;

public interface ILog
{
    void Info(string msg);

    void Warning(string msg);

    void Debug(string msg);

    void Exception(string msg, Exception ex);
}

