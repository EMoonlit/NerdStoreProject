using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

public class AuthController : Controller
{
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
        
        // if error return View(userRegister);
        
        // Login success -> return RedirectToAction("Index", "controllerName")
        
        return await Ok();
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
        
        // if error return View(userLogin);
        
        // Login success -> return RedirectToAction("Index", "controllerName")
        
        return await Ok();
    }

    [HttpGet]
    [Route("exit")]
    public async Task<IActionResult> Logout()
    {
        // success -> return RedirectToAction("Index", "controllerName")
    }
}
