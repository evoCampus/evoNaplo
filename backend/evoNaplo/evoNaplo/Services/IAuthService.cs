using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services;

public interface IAuthService
{
    RegisterDTO Register(RegisterDTO registerData);
    LoginDTO? Login(LoginDTO loginData);

}
