using AdminControlPanel.BL.Interfaces;
using Entities.CustomerApi.CustomerData;
using Entities.General;
using Microsoft.AspNetCore.Mvc;

namespace AdminControlPanel.Controllers;

[ApiController]
public class CustomerControlPanel : ControllerBase
{
    private readonly IAccountLogicForAdmin _accountLogicForAdmin;

    public CustomerControlPanel(IAccountLogicForAdmin accountLogicForAdmin)
    {
        _accountLogicForAdmin = accountLogicForAdmin;
    }

    [HttpPost("/GetAllCustomersWithId")]
    public async Task<List<CustomerDB>> GetAllCustomersWithId()
    { 
        return await _accountLogicForAdmin.GetAllUsersWithId();
    }
    
    [HttpPost("/ChangeStatusOfUser")]
    public async Task<Response> ChangeStatusOfUser(string phoneNumber, AccountStatus newStatus)
    {
        return await _accountLogicForAdmin.ChangeAccountStatus(phoneNumber, newStatus);
    }
    
    [HttpPost("/DeleteUser")]
    public async Task<Response> DeleteUser(int userId)
    {
        return await _accountLogicForAdmin.DeleteUserById(userId);
    }
}