namespace evoNaplo.Exceptions;

/// <summary>
/// Custom exception class to indicate that a student with the same attributes already exists in the database. This exception is thrown when an attempt is made to add a new student that has the same ID as an existing student, or when the provided student data conflicts with existing students in a way that violates uniqueness constraints. The exception message provides details about the specific conflict, allowing for better error handling and debugging in the application.
/// </summary>
public class StudentAlreadyExistsException : Exception
{
    public StudentAlreadyExistsException(string message) : base(message)
    {
        
    }
}
