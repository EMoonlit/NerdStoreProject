using Microsoft.Extensions.Options;
using NSE.Identity.API.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class AuthService : Validation, IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient, IOptions<AppSettings> appSettings)
    {
        httpClient.BaseAddress = new Uri(appSettings.Value.AuthUrl);
        _httpClient = httpClient;
    }
    
    public async Task<UserLoginResponse> Login(UserLogin userLogin)
    {
        var loginContent = GetContent(userLogin);
        
        var response = await _httpClient.PostAsync("/api/identity/auth", loginContent);
        
        
        if (!ValidateErrorResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeResponseObjectAsync<ResponseResult>(response) 
            };
        }

        return await DeserializeResponseObjectAsync<UserLoginResponse>(response);
    }

    public async Task<UserLoginResponse> Register(UserRegister userRegister)
    {
        var userContent = GetContent(userRegister);
        
        var response = await _httpClient.PostAsync("/api/identity/new-account", userContent);
        
        if (!ValidateErrorResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeResponseObjectAsync<ResponseResult>(response)
            };
        }
        return await DeserializeResponseObjectAsync<UserLoginResponse>(response);
    }
}