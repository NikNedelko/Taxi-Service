using Application.BL.Customer.Interfaces;
using Domain.Entities.CustomerApi.Requests;
using Domain.Entities.General;
using Domain.Entities.General.RideData;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

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
    public async Task<List<RideDb>> GetAllRides()
    {
        return await _createOrdersLogic.GetAllRides();
    }
}