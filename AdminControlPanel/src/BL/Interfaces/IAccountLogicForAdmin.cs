using Entities.CustomerTaxiService.CustomerData;
using Entities.General;
using TaxiService.BusinessLogic.Customer.Interfaces;

namespace AdminControlPanel.BL.Interfaces;

public interface IAccountLogicForAdmin : IAccountLogic
{
    public Task<Response> DeleteUserById(int id);
    public Task<List<CustomerDB>> GetAllUsersWithId();
    public Task<Response> ChangeAccountStatus(string phoneNumber, AccountStatus newAccountStatus);
}