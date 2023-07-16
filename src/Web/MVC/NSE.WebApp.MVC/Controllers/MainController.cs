using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

public class MainController : Controller
{
    protected bool IsResponseError(ErrorResponse response)
    {
        if (response != null && response.Errors.Messages.Any())
        {
            foreach (var message in response.Errors.Messages)
            {
                ModelState.AddModelError(string.Empty, message);
            }
            
            return true;
        }

        return false;
    }
}