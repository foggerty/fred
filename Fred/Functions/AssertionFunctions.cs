using System.Reflection;
using Fred.Exceptions;

namespace Fred.Functions;

internal static class AssertionFunctions
{
    internal const string ThisDoesNoptImplimentThat = "{0} does not implement {1}.  Nor does it compliment it, or even give it the time of day.";
    internal const string NotAnInterface = "This needs to be an interface.  Not wants to be, needs to be.";
    internal const string NoPublicConstructor = "There is no public or default constructor for type {0}, and yet you assured me that there was.  Why the deceit?";    
   
    internal static void MustBeInterface(this Type t)
    {
        if(!t.IsInterface)
            throw new DeveloperException(NotAnInterface);
    }

    internal static void MustImpliment(this Type t, Type i)
    {
        if(!t.GetTypeInfo().ImplementedInterfaces.Contains(i))
            throw new DeveloperException(ThisDoesNoptImplimentThat, t.Name, i.Name);
    }

    internal static void MustHavePublicConstructor(this Type t)
    {
        if(t.HasEmptyConstructor() || t.HasSinglePublicConstructor())
            return;

        throw new DeveloperException(NoPublicConstructor, t.Name);
    }    
}