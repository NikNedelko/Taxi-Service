using Microsoft.AspNetCore.Mvc;
using CustomerTaxiService.BusinessLogic;

namespace CustomerTaxiService.Controllers;

[ApiController]
public class Order : ControllerBase
{
    private readonly CreateOrderLogic _createOrderLogic;

    public Order(CreateOrderLogic createOrderLogic)
    {
        _createOrderLogic = createOrderLogic;
    }
    
    [HttpPost("/RequestARide")]
    public async Task<string> CreateOrder(string usersNumber)
    {
        _ = _createOrderLogic.BeginNewOrder(usersNumber);
        return "";
    }
    
    [HttpPost("/CancelOrder")]
    public async Task<string> CancelOrder(string str)
    {
        _ = _createOrderLogic.BeginNewOrder(str);
        return "";
    }
}