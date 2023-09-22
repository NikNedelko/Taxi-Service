using Entities.General;
using Entities.General.RideData;
using Microsoft.AspNetCore.Mvc;
using TaxiService.BusinessLogic.DriverLogic.Interface;

namespace TaxiService.Controllers;

[ApiController]
public class DriverDriveController : ControllerBase
{
    private readonly IDriveLogic _driveLogic;

    public DriverDriveController(IDriveLogic driveLogic)
    {
        _driveLogic = driveLogic;
    }
    
    [HttpPost("/StartWork")]
    public async Task<Response> StartWork(string phoneNumber)
    {
        return await _driveLogic.StartWork(phoneNumber);
    }
    
    [HttpPost("/EndWork")]

    public async Task<Response> EndWork(string phoneNumber)
    {
        return await _driveLogic.EndWork(phoneNumber);
    }
    
    [HttpPost("/GetAllAvailableRequests")]

    public async Task<List<RideDb>> GetAllAvailableRequests(string phoneNumber)
    {
        return await _driveLogic.GetAllAvailableOrders(phoneNumber);
    }
    
    [HttpPost("/TakeOrderById")]

    public async Task<Response> TakeOrderById(int rideId, string phoneNumber)
    {
        return await _driveLogic.TakeOrderById(rideId,phoneNumber);
    }
    
    [HttpPost("/EndOrder")]
    public async Task<Response> EndOrder(string phoneNumber)
    {
        return await _driveLogic.EndOrder(phoneNumber);
    }
}