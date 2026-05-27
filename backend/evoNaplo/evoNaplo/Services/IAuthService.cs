using evoNaplo.DTO;

namespace evoNaplo.Services;

public interface IAuthService
{
    Task<UserDTO> RegisterAsync(RegisterDTO registerData);
    Task<UserDTO> LoginAsync(LoginDTO loginData);

}
