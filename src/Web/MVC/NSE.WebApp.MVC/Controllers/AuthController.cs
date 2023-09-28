using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers;

public class AuthController : MainController
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
        
        if (IsResponseError(response.ResponseResult)) return View(userRegister);
        
        // Active Login
        await ActiveLogin(response);

        return RedirectToAction("Index", "Home");;
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");;
        return View();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLogin userLogin, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
        if (!ModelState.IsValid) return View(userLogin);

        var response = await _authService.Login(userLogin);

        if (IsResponseError(response.ResponseResult)) return View(userLogin);

        await ActiveLogin(response);

        if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

        return LocalRedirect(returnUrl);
        
    }


    [HttpGet]
    [Route("exit")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");;
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
        return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken) as JwtSecurityToken;
    }
}
