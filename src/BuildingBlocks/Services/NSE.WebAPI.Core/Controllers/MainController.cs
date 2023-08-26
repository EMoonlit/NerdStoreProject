using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.WebAPI.Core.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Errors = new List<string>();
    
    protected ActionResult CustomResponse(object result = null)
    {
        if (!IsValidOperation())
        {
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        return Ok(result);
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var error in errors)
        {
            AddProcessError(error.ErrorMessage);
        }

        return CustomResponse();
    }
    
    protected ActionResult CustomResponse(ValidationResult modelState)
    {
        foreach (var error in modelState.Errors)
        {
            AddProcessError(error.ErrorMessage);
        }

        return CustomResponse();
    }


    protected bool IsValidOperation()
    {
        return !Errors.Any();
    }

    protected void AddProcessError(string error)
    {
        Errors.Add(error);
    }

    protected void ClearProcessErrors()
    {
        Errors.Clear();
    }
}