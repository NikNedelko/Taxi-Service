using Entities.DriverApi.DriverData;
using Entities.General;
using Microsoft.AspNetCore.Mvc;
using TaxiService.BusinessLogic.DriverLogic.Interface;

namespace TaxiService.Controllers;

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