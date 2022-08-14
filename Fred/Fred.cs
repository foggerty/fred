namespace Fred;

public static class Fred
{
    public static Func<string> HostName = () => "localhost";

    public static Func<int> Port = () => 8080;
}