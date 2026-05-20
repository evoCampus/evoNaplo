namespace evoNaplo.Exceptions;

/// <summary>
/// Custom exception class to indicate that a student was not found in the database. This exception is thrown when an operation attempts to access a student that does not exist, such as retrieving, updating, or deleting a student by its ID. The exception message provides details about the specific student that was not found, allowing for better error handling and debugging in the application.
/// </summary>
public class StudentNotFoundException : Exception
{
    public StudentNotFoundException(string message) : base(message)
    {
        
    }
}
