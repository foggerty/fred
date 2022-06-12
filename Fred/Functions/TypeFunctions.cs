using Fred.Exceptions;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Fred.Functions;

internal static class TypeFunctions
{
    private const string NeedSingleConstructor = "{0} needs either a default (empty) constuctor or a single public constructor.  I get confused otherwise.  Sorry.";
    private const string ReallyNeedSingleConstructor = "{0} needs EITHER a default (empty) constuctor OR a single public constructor.  I get confused otherwise.  Sorry.";

    public static bool HasEmptyConstructor(this Type t)
    {
        return t.EmptyConstructor() != null;
    }
    
    public static ConstructorInfo? EmptyConstructor(this Type t)
    {
        var constructor = t.GetConstructor(Array.Empty<Type>());

        return constructor ?? null;
    }

    public static bool HasSinglePublicConstructor(this Type t)
    {
        return t.PublicConstructor() != null;
    }

    public static ConstructorInfo? PublicConstructor(this Type t)
    {
        var publicConstructors = t
            .GetConstructors()
            .Where(c => c.IsPublic)
            .Where(c => c.GetParameters()?.Length > 0);
        
        if(publicConstructors.Count() != 1)
            return null;

        var constructor = publicConstructors
            .First();
        
        var allInterfaces = constructor
            .GetParameters()
            .All(p => p.ParameterType.IsInterface);        
        
        return allInterfaces
            ? constructor 
            : null;
    }

    public static ConstructorInfo? DefaultConstructor(this Type t)
    {
        var emptyConstructor = t.EmptyConstructor();
        var publicConstructor = t.PublicConstructor();

        if(emptyConstructor == null && publicConstructor == null)
            throw new DeveloperException(NeedSingleConstructor, t.Name);
        
        if(emptyConstructor != null && publicConstructor != null)
            throw new DeveloperException(ReallyNeedSingleConstructor, t.Name);
        
        return emptyConstructor ?? publicConstructor;
    }
}