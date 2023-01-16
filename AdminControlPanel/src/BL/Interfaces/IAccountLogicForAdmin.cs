using CustomerTaxiService.BusinessLogic.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.General;

namespace AdminControlPanel.BL.Interfaces;

public interface IAccountLogicForAdmin : IAccountLogic
{
    public Task<Response> DeleteUserById(int id);
    public Task<List<CustomerDB>> GetAllUsersWithId();
    public Task<Response> ChangeAccountStatus(string phoneNumber, AccountStatus newAccountStatus);
}