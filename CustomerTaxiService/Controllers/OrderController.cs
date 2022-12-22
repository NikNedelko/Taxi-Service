using Microsoft.AspNetCore.Mvc;
using CustomerTaxiService.BusinessLogic;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;

namespace CustomerTaxiService.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderLogic _createOrderLogic;

    public OrderController(OrderLogic createOrderLogic)
    {
        _createOrderLogic = createOrderLogic;
    }

    [HttpPost("/RequestARide")]
    public async Task<Response> CreateOrder(Order order)
    {
        return await _createOrderLogic.BeginNewOrder(order);
    }

    [HttpPost("/CancelOrder")]
    public async Task<string> CancelOrder(string str)
    {
        _ = await _createOrderLogic.CancelOrder(str);
        return "";
    }

    public async Task<Response> GetInformationAboutRide(string rideId) // also by phone number
    {
        throw new NotImplementedException();
    }
}