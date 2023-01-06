using DriverTaxiService.BusinessLogic.Interface;
using Entities.General;
using Microsoft.AspNetCore.Mvc;

namespace DriverTaxiService.Controllers;

[ApiController]
public class DriveController : ControllerBase
{
    private readonly IDriveLogic _driveLogic;

    public DriveController(IDriveLogic driveLogic)
    {
        _driveLogic = driveLogic;
    }

    public async Task<Response> StartWork(string phoneNumber)
    {
        return await _driveLogic.StartWork(phoneNumber);
    }
    
    public async Task<Response> EndWork(string phoneNumber)
    {
        return await _driveLogic.EndWork(phoneNumber);
    }
    
    public async Task<Response> GetAllAvailableRequests(string phoneNumber)
    {
        return await _driveLogic.StartWork(phoneNumber);
    }
    
    public async Task<Response> TakeOrderById(string id)
    {
        
    }
    
    public async Task<Response> EndOrder(string phoneNumber)
    {

    }
}