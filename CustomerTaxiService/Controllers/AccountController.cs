using CustomerTaxiService.BusinessLogic.Interfaces;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;
using Microsoft.AspNetCore.Mvc;

namespace CustomerTaxiService.Controllers;

[ApiController]
public class AccountController : ControllerBase
{

    private readonly IAccountLogic _accountLogic;
    
    public AccountController(IAccountLogic accountLogic)
    {
        _accountLogic = accountLogic;
    }
    
    [HttpPost]
    public async Task<Response> CreateAccount(Registration newUser)
    {
        return await _accountLogic.CreateAccount(newUser);
    }

    [HttpPost]
    public async Task<Response> DeleteAccount(string phoneNumber)
    {
        return new Response();
    }

    [HttpPost]
    public async Task<Response> UpdateAccount(string account)
    {
        return new Response();
    }

    [HttpPost]
    public async Task<Response> AddMoneyToAccount(decimal countOfMoney, string accountId)
    {
        return new Response();
    }
}