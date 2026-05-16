namespace evoNaplo.Exceptions;

/// <summary>
/// Custom exception class to indicate that a mentor with the same attributes already exists in the database. This exception is thrown when an attempt is made to add a new mentor that has the same ID as an existing mentor, or when the provided mentor data conflicts with existing mentors in a way that violates uniqueness constraints. The exception message provides details about the specific conflict, allowing for better error handling and debugging in the application.
/// </summary>
public class MentorAlreadyExistsException : Exception
{
    public MentorAlreadyExistsException(string message) : base(message)
    {
        
    }
}
