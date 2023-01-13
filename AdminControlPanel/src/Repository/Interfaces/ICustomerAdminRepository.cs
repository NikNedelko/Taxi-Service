using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.General;

namespace AdminControlPanel.Repository.Interfaces;

public interface ICustomerAdminRepository : IUserRepository
{
    public Task<List<CustomerDB>> GetAllUserWIthId();
    public Task<string> DeleteUserById(int userId);
    public Task<CustomerDB?> GetUserById(int userId);
    public Task<string> ChangeCustomerStatus(string phoneNumber, AccountStatus newAccountStatus);
}