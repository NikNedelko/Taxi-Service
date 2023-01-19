using Entities.CustomerApi.Requests;
using Entities.General;
using Entities.General.RideData;
using Microsoft.AspNetCore.Mvc;
using TaxiService.BusinessLogic.Customer.Interfaces;

namespace TaxiService.Controllers;

[ApiController]
public class CustomerOrderController : ControllerBase
{
    private readonly IOrdersLogic _createOrdersLogic;

    public CustomerOrderController(IOrdersLogic createOrdersLogic)
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