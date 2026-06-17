namespace evoNaplo.Exceptions;

public class UserWithEmailAlreadyExistsException : Exception
{
    public UserWithEmailAlreadyExistsException(string message) : base(message)
    {
        
    }
}
