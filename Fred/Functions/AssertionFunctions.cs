using Fred.Exceptions;

namespace Fred.Functions;

internal static class AssertionFunctions
{
    internal static void MustBeInterface(this Type t)
    {
        if(!t.IsInterface)
            throw new DeveloperException("This needs to be an interface.");
    }
}