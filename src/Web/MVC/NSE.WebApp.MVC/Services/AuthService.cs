using System.Text;
using System.Text.Json;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class AuthService : IAuthService
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

        var test = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync());
    }

    public async Task<UserLoginResponse> Register(UserRegister userRegister)
    {
        var userContent = new StringContent(
            JsonSerializer.Serialize(userRegister),
            Encoding.UTF8,
            "application/json"
        );
        
        var response = await _httpClient.PostAsync("https://localhost:7016/api/identity/new-account", userContent);

        var content = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync());
    }
}