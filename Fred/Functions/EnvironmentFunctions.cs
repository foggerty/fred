namespace Fred.Functions;

public static class EnvironmentFunctions
{
    public static Func<string> HostName = () => "localhost";

    public static Func<int> Port = () => 8080;
}