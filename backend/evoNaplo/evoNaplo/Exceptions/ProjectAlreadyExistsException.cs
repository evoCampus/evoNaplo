namespace evoNaplo.Exceptions;

/// <summary>
/// Custom exception class to indicate that a project with the same attributes already exists in the database. This exception is thrown when an attempt is made to add a new project that has the same ID as an existing project, or when the provided project data conflicts with existing projects in a way that violates uniqueness constraints. The exception message provides details about the specific conflict, allowing for better error handling and debugging in the application.
/// </summary>
public class ProjectAlreadyExistsException : Exception
{
    public ProjectAlreadyExistsException(string message) : base(message)
    {
        
    }
}
