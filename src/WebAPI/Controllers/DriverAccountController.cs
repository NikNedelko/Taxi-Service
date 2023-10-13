using DAL.Interfaces.Driver;
using Domain.Entities.DriverData;
using Domain.Entities.General;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
public class DriverAccountController : ControllerBase
{
    private readonly IDriverAccountLogic _accountLogic;
    
    public DriverAccountController(IDriverAccountLogic accountLogic)
    {
        _accountLogic = accountLogic;
    }
    
    [HttpPost("/CreateDriverAccount")]
    public async Task<Response> CreateDriverAccount(RegistrationForDriver registrationDriver)
    {
        return await _accountLogic.AddNewDriver(registrationDriver);
    }
    
    [HttpPost("/DeleteDriverAccount")]
    public async Task<Response> DeleteDriverAccount(string phoneNumber)
    {
        return  await _accountLogic.DeleteDriver(phoneNumber);
    }
}