namespace evoNaplo.Exceptions;

/// <summary>
/// Custom exception class to indicate that a team was not found in the database. This exception is thrown when an operation attempts to access a team that does not exist, such as retrieving, updating, or deleting a team by its ID. The exception message provides details about the specific team that was not found, allowing for better error handling and debugging in the application.
/// </summary>
public class TeamNotFoundException : Exception
{
    public TeamNotFoundException(string message) : base(message)
    {
        
    }
}
