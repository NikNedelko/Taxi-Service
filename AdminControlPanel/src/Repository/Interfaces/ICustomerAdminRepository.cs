using Entities.CustomerApi.CustomerData;
using Entities.General;
using TaxiService.Repository.Customer.Interfaces;

namespace AdminControlPanel.Repository.Interfaces;

public interface ICustomerAdminRepository : IUserRepository
{
    public Task<List<CustomerDB>> GetAllUserWIthId();
    public Task<string> DeleteUserById(int userId);
    public Task<CustomerDB?> GetUserById(int userId);
    public Task<string> UpdateUserAsAdmin(Customer user, int existUserId);
    public Task<string> ChangeCustomerStatus(string phoneNumber, AccountStatus newAccountStatus);
}