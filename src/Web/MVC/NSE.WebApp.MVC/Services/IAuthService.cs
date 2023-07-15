using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public interface IAuthService
{
    Task<UserLoginResponse> Login(UserLogin userLogin);

    Task<UserLoginResponse> Register(UserRegister userRegister);
}