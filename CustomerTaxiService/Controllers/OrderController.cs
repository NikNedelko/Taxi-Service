using Microsoft.AspNetCore.Mvc;
using CustomerTaxiService.BusinessLogic.Interfaces;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;

namespace CustomerTaxiService.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrdersLogic _createOrdersLogic;

    public OrderController(IOrdersLogic createOrdersLogic)
    {
        _createOrdersLogic = createOrdersLogic;
    }

    [HttpPost("/RequestARide")]
    public async Task<Response> CreateOrder(Order order)
    {
        return await _createOrdersLogic.BeginNewOrder(order);
    }

    [HttpPost("/CancelOrder")]
    public async Task<Response> CancelOrder(int rideId)
    {
        return await _createOrdersLogic.CancelOrder(rideId);
    }

    [HttpPost("/GetInformationAboutRide")]
    public async Task<Response> GetInformationAboutRide(string rideId) // also by phone number
    {
        throw new NotImplementedException();
    }
}