namespace evoNaplo.DTO;

public enum AuthCode
{
    Success,
    InvalidCredentials,
    EmailAlreadyInUse,
    MentorNotFound,

}

public class AuthResultDTO
{
    public AuthCode AuthCode { get; init; }
    public UserDTO? User { get; init; }
    public string? Message { get; init; }

    public AuthResultDTO(AuthCode code)
    { 
        AuthCode = code;
    }

    public AuthResultDTO(AuthCode code, UserDTO user) : this(code)
    {
        User = user;
    }

    public AuthResultDTO(AuthCode code, string message) : this(code)
    { 
        Message = message;
    }

    public AuthResultDTO(AuthCode code, UserDTO user, string message) : this(code, user)
    { 
        Message = message;
    }

}
