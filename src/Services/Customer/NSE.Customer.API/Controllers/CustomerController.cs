using Microsoft.AspNetCore.Mvc;
using NSE.Core.Mediator;
using NSE.Customer.API.Application.Commands;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Customer.API.Controllers;

public class CustomerController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public CustomerController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }
    
    [HttpGet("customers")]
    public async Task<ActionResult> Index()
    {
        var result = await _mediatorHandler.SendCommand(new RegisterCustomerCommand(
            Guid.NewGuid(),
            "Emerson",
            "emerson@moonlit.com",
            "00000000191"
        ));
        
        return CustomResponse(result);
    }
}