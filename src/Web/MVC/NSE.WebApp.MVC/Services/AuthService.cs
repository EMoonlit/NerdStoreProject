using System.Text;
using System.Text.Json;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class AuthService : Validation, IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<UserLoginResponse> Login(UserLogin userLogin)
    {
        var loginContent = new StringContent(
            JsonSerializer.Serialize(userLogin),
            Encoding.UTF8,
            "application/json"
            );
        
        var response = await _httpClient.PostAsync("https://localhost:7016/api/identity/auth", loginContent);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        
        if (!ValidateErrorResponse(response))
        {
            return new UserLoginResponse
            {
                ErrorResponse =
                    JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options)
            };
        }

        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
    }

    public async Task<UserLoginResponse> Register(UserRegister userRegister)
    {
        var userContent = new StringContent(
            JsonSerializer.Serialize(userRegister),
            Encoding.UTF8,
            "application/json"
        );
        
        var response = await _httpClient.PostAsync("https://localhost:7016/api/identity/new-account", userContent);
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        
        if (!ValidateErrorResponse(response))
        {
            return new UserLoginResponse
            {
                ErrorResponse =
                    JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options)
            };
        }
        
        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
    }
}