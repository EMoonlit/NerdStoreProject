using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpGet]
    [Route("new-account")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Route("new-account")]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        if (!ModelState.IsValid) return View(userRegister);
        
        // Send User to Identity API
        var response = await _authService.Register(userRegister);
        
        // if error return View(userRegister);
        
        // Active Login
        await ActiveLogin(response);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        if (!ModelState.IsValid) return View(userLogin);
        
        // Send User to Identity API
        var response = await _authService.Login(userLogin);
        
        // if error return View(userLogin);
        
        // Active Login in App
        await ActiveLogin(response);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("exit")]
    public async Task<IActionResult> Logout()
    {
        // success -> return RedirectToAction("Index", "controllerName")
        return RedirectToAction("Index", "Home");
    }

    private async Task ActiveLogin(UserLoginResponse userLoginResponse)
    {
        var token = FormatToken(userLoginResponse.AccessToken);

        var claims = new List<Claim> { new("JWT", userLoginResponse.AccessToken) };
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true
        };
        
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), 
            authProperties);
    }

    private static JwtSecurityToken FormatToken(string jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
    }
}
