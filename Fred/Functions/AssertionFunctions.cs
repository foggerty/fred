using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;

namespace Fred.Functions;

internal static class AssertionFunctions
{
    private const string NotAnInterface = "This needs to be an interface.  Not wants to be, needs to be.";

    private const string ThisDoesNotImplementThat =
        "{0} does not implement {1}.  Nor does it compliment it, or even give it the time of day.";

    private const string NotAnAllowedService = "{0} is not on the list of allowed services.  Fred says no.";

    private const string NotAClass = "I'm awfuly sorry, and to be perfectly honest the compiler should have picked up on this before even allowing you to run this code.";

    public static void MustBeClass(this Type t)
    {
        if (t.IsClass)
            return;

        throw new DeveloperException(NotAClass);
    }
    
    public static void MustBeInterface(this Type t)
    {
        if (t.IsInterface)
            return;

        throw new DeveloperException(NotAnInterface);
    }

    public static void MustImplement(this Type t, Type i)
    {
        if (t.GetTypeInfo().ImplementedInterfaces.Contains(i))
            return;

        throw new DeveloperException(ThisDoesNotImplementThat, t.Name, i.Name);
    }

    public static void MustHaveDIFriendlyConstructor(this Type t)
    {
        // The following will throw an exception if there is no appropriate constructor to use with Fred's
        // DI container.

        t.DefaultConstructorForDi();
    }

    public static void MustBeAllowedService(this Type t)
    {
        if (t.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IFredService)))
            return;

        throw new DeveloperException(NotAnAllowedService, t.Name);
    }
}