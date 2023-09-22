using Entities.CustomerApi.CustomerData;
using Entities.CustomerApi.Requests;
using Entities.General;
using Microsoft.AspNetCore.Mvc;
using TaxiService.BusinessLogic.Customer.Interfaces;

namespace TaxiService.Controllers;

[ApiController]
public class CustomerAccountController : ControllerBase
{
    private readonly IAccountLogic _accountLogic;

    public CustomerAccountController(IAccountLogic accountLogic)
    {
        _accountLogic = accountLogic;
    }

    [HttpPost("/CreateAccount")]
    public async Task<Response> CreateAccount(RegistrationForUser newUser)
    {
        return await _accountLogic.CreateAccount(newUser);
    }

    [HttpPost("/DeleteAccount")]
    public async Task<Response> DeleteAccount(string phoneNumber)
    {
        return await _accountLogic.DeleteAccount(phoneNumber);
    }

    [HttpPost("/UpdateAccount")]
    public async Task<Response> UpdateAccount(Customer customerEntity)
    {
        return await _accountLogic.UpdateAccount(customerEntity);
    }

    [HttpPost("/AddMoneyToAccount")]
    public async Task<Response> AddMoneyToAccount(string phoneNumber, decimal countOfMoney)
    {
        return await _accountLogic.AddMoneyToAccount(phoneNumber, countOfMoney);
    }
}