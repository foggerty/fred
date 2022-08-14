namespace Fred.Functions;

public static class StringFunctions
{
    public static bool EqualsThumbprint(this string test, string thumbprint)
    {
        return Tidy(test).Equals(Tidy(thumbprint));
    }

    private static string Tidy(string thumbprint)
    {
        return thumbprint
               .Trim()
               .Replace(" ", "")
               .Replace("-", "")
               .ToUpper(CultureInfo.CurrentCulture);
    }
}