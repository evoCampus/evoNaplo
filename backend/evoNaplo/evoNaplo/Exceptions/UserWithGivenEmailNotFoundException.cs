namespace evoNaplo.Exceptions;

public class UserWithGivenEmailNotFoundException : Exception
{
    public UserWithGivenEmailNotFoundException(string message) : base(message)
    {
        
    }
}
