using Fred.Exceptions;

namespace Fred.Functions;

internal static class TypeFunctions
{
    private const string NeedSingleConstructor = "{0} needs either a default (empty) constuctor or a single public constructor to be used in Fred's DI container.  He gets confused otherwise.  Sorry.";
    private const string ReallyNeedSingleConstructor = "{0} needs EITHER a default (empty) constuctor OR a single public constructor to be used in Fred's DI container.  He get confused otherwise.  Sorry.";
    
    public static ConstructorInfo? EmptyConstructor(this Type t)
    {
        return t.GetConstructor(Array.Empty<Type>());
    }

    public static ConstructorInfo? NonEmptyConstructor(this Type t)
    {
        return t
            .GetConstructors()
            .Where(c => c.IsPublic)
            .Where(c => c.GetParameters()?.Length > 0)
            .Where(c => c.GetParameters().All(p => p.ParameterType.IsInterface))
            .FirstOrDefault();        
    }

    public static ConstructorInfo DefaultConstructorForDi(this Type t)
    {
        var emptyConstructor = t.EmptyConstructor();
        var publicConstructor = t.NonEmptyConstructor();

        if(emptyConstructor == null && publicConstructor == null)
            throw new DeveloperException(NeedSingleConstructor, t.Name);
        
        if(emptyConstructor != null && publicConstructor != null)
            throw new DeveloperException(ReallyNeedSingleConstructor, t.Name);
        
        #pragma warning disable CS8603
        return emptyConstructor ?? publicConstructor;
    }
}