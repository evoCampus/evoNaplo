namespace evoNaplo.Exceptions;

/// <summary>
/// Custom exception class to indicate that a mentor was not found in the database. This exception is thrown when an operation attempts to access a mentor that does not exist, such as retrieving, updating, or deleting a mentor by its ID. The exception message provides details about the specific mentor that was not found, allowing for better error handling and debugging in the application.
/// </summary>
public class MentorNotFoundException : Exception
{
    public MentorNotFoundException(string message) : base(message)
    {
        
    }
}
