namespace evoNaplo.Exceptions;

/// <summary>
/// Custom exception class to indicate that a team with the same attributes already exists in the database. This exception is thrown when an attempt is made to add a new team that has the same ID as an existing team, or when the provided team data conflicts with existing teams in a way that violates uniqueness constraints. The exception message provides details about the specific conflict, allowing for better error handling and debugging in the application.
/// </summary>
public class TeamAlreadyExistsException : Exception
{
    public TeamAlreadyExistsException(string message) : base(message)
    {
        
    }
}
