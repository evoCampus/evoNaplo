namespace evoNaplo.Exceptions;

/// <summary>
/// Custom exception class to indicate that a project was not found in the database. This exception is thrown when an operation attempts to access a project that does not exist, such as retrieving, updating, or deleting a project by its ID. The exception message provides details about the specific project that was not found, allowing for better error handling and debugging in the application.
/// </summary>
public class ProjectNotFoundException : Exception
{
    public ProjectNotFoundException(string message) : base(message)
    {
        
    }
}
