using DriverTaxiService.BusinessLogic.Interface;
using Entities.DriverApi.Driver;
using Entities.General;
using Microsoft.AspNetCore.Mvc;

namespace DriverTaxiService.Controllers;

[ApiController]
public class DriverAccountController : ControllerBase
{
    private readonly IAccountLogic _accountLogic;
    
    public DriverAccountController(IAccountLogic accountLogic)
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
    
    [HttpPost("/GetAllDrivers")]
    public async Task<List<DriverDB>> GetAllDrivers()
    {
        return await _accountLogic.GetAllDrivers();
    }
}