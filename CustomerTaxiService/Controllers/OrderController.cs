using Microsoft.AspNetCore.Mvc;
using CustomerTaxiService.BusinessLogic.Interfaces;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;
using Entities.CustomerTaxiService.RideData;

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
    public async Task<Response> CancelOrder(string phoneNumber)
    {
        return await _createOrdersLogic.CancelOrder(phoneNumber);
    }

    [HttpPost("/GetInformationAboutRide")]
    public async Task<Ride?> GetInformationAboutRide(string phoneNumber)
    {
        return await _createOrdersLogic.GetRideInfo(phoneNumber);
    }
    
    [HttpPost("/GetAllRIdes")]
    public async Task<List<RideDb>> GetAllUsers()
    {
        return await _createOrdersLogic.GetAllRides();
    }
}