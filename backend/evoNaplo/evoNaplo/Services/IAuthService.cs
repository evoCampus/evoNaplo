using evoNaplo.DTO;

namespace evoNaplo.Services;

public interface IAuthService
{
    Task<AuthResultDTO> RegisterAsync(RegisterDTO registerData);
    Task<AuthResultDTO> LoginAsync(LoginDTO loginData);

}
