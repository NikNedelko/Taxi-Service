using Entities.DriverApi.Driver;
using Microsoft.AspNetCore.Mvc;

namespace DriverTaxiService.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    public async Task<string> CreateDriverAccount(RegistrationForDriver registrationDriver)
    {
        return "";
    }
    
    public async Task<string> DeleteDriverAccount(string phoneNumber)
    {
        return  "";
    }
}