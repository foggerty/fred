using Fred.Exceptions;

namespace Fred.Functions;

internal static class AssertionFunctions
{
    private const string NotAnInterface = "This needs to be an interface.  Not wants to be, needs to be.";
    private const string ThisDoesNoptImplimentThat = "{0} does not implement {1}.  Nor does it compliment it, or even give it the time of day.";    
   
    public static void MustBeInterface(this Type t)
    {
        if(t.IsInterface)
            return;

        throw new DeveloperException(NotAnInterface);
    }

    public static void MustImpliment(this Type t, Type i)
    {
        if(t.GetTypeInfo().ImplementedInterfaces.Contains(i))
            return;
        
        throw new DeveloperException(ThisDoesNoptImplimentThat, t.Name, i.Name);
    }

    public static void MustHaveDiConstructor(this Type t)
    {
        t.DefaultConstructorForDi();
    }
}