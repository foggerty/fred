namespace Fred.Abstractions.PublicFacing.Services;

public interface IFredService
{
    // All user-supplied services must implement this interface before
    // Fred will even consider using them.
    
    /* Rationale:
       
       Show that you put minimal effort into integrating your service into the framework.
       
       By forcing users to put this interface in front of all services used by the
       framework, it a) ensures that they don't start building dependencies on
       3rd party interfaces and b) maybe they'll actually thing about WHY they're 
       adding the service a bit.
       
       I can hope.  
        
     */
}