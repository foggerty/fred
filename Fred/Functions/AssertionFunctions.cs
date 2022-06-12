using Fred.Exceptions;

namespace Fred.Functions;

internal static class AssertionFunctions
{
    private const string ThisDoesNoptImplimentThat = "{0} does not implement {1}.  Nor does it compliment it, or even give it the time of day.";
    private const string NotAnInterface = "This needs to be an interface.  Not wants to be, needs to be.";
    private const string NoPublicConstructor = "There is no public or default constructor for type {0}, and yet you assured me that there was.  Why the deceit?";
    private const string TypeNeedsEmptyConstructor = "The type {0} needs a (SINGLE) default constructor.  While I am sure that your reason for hiding it from me is both valid and correct, I cannot, alas, conrinue without it.";
   
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

    public static void MustHaveEmptyConstructor(this Type t)
    {
        if(t.HasEmptyConstructor())
            return;
        
        throw new DeveloperException(TypeNeedsEmptyConstructor, t.Name);
    }
       
    public static void MustHavePublicConstructor(this Type t)
    {
        if(t.HasEmptyConstructor() || t.HasPublicConstructor())
            return;

        throw new DeveloperException(NoPublicConstructor, t.Name);
    }
}