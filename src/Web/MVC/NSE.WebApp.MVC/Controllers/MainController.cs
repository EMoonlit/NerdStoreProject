using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

public class MainController : Controller
{
    protected bool IsResponseError(ResponseResult response)
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

    protected void AddValidationError(string message)
    {
        ModelState.AddModelError(String.Empty, message);
    }

    protected bool IsValidOperation()
    {
        return ModelState.ErrorCount == 0;
    }
}