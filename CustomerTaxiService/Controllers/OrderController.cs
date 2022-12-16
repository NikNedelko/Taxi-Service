using Microsoft.AspNetCore.Mvc;
using CustomerTaxiService.BusinessLogic;
using Entities.CustomerTaxiService.Requests;

namespace CustomerTaxiService.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly CreateOrderLogic _createOrderLogic;

    public OrderController(CreateOrderLogic createOrderLogic)
    {
        _createOrderLogic = createOrderLogic;
    }
    
    [HttpPost("/RequestARide")]
    public async Task<string> CreateOrder(Order order)
    {
        _ = await _createOrderLogic.BeginNewOrder(order);
        return "";
    }
    
    [HttpPost("/CancelOrder")]
    public async Task<string> CancelOrder(string str)
    {
        _ = await _createOrderLogic.CancelOrder(str);
        return "";
    }
}