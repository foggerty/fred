using System.Reflection;
using Fred.Exceptions;

namespace Fred.Functions;

internal static class AssertionFunctions
{
    internal static void MustBeInterface(this Type t)
    {
        if(!t.IsInterface)
            throw new DeveloperException("This needs to be an interface.  Not wants to be, needs to be.");
    }

    internal static void MustImpliment(this Type t, Type i)
    {
        if(!t.GetTypeInfo().ImplementedInterfaces.Contains(i))
            throw new DeveloperException("t does not implement i.  Nor does it compliment it, or even give it the time of day.");
    }
}