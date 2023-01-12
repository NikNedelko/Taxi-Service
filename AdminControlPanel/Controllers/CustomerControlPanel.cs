using System.Text.RegularExpressions;
using AdminControlPanel.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.General;
using Microsoft.AspNetCore.Mvc;

namespace AdminControlPanel.Controllers;

[ApiController]
public class CustomerControlPanel : ControllerBase
{
    private IAccountLogicForAdmin _accountLogicForAdmin;

    public CustomerControlPanel(IAccountLogicForAdmin accountLogicForAdmin)
    {
        _accountLogicForAdmin = accountLogicForAdmin;
    }

    [HttpPost("/GetAllCustomersWithId")]
    public async Task<List<CustomerDB>> GetAllCustomersWithId()
    { 
        return await _accountLogicForAdmin.GetAllUsers();
    }
    
    [HttpPost("/ChangeStatusOfUser")]
    public async Task<List<CustomerDB>> ChangeStatusOfUser(string userId, AccountStatus newStatus)
    {
        return View();
    }
    
    [HttpPost("/DeleteUser")]
    public async Task<List<CustomerDB>> DeleteUser(string userId)
    {
        return View();
    }
}