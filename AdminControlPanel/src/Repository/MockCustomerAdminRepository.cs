using AdminControlPanel.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;

namespace AdminControlPanel.Repository;

public abstract class MockCustomerAdminRepository : ICustomerAdminRepository
{
    
    
    public abstract Task<string> AddNewUser(Customer customer);
    public abstract Task<string> RemoveUser(string phoneNumber);
    public abstract Task<Customer?> GetUserByPhoneNumber(string number);
    public abstract Task<string?> PermissionToRide(string userId);
    public abstract Task<string> CheckOfExist(string phoneNumber);
    public abstract Task<string> UpdateUser(Customer user, string existUserId);
    public abstract Task<string> AddMoneyToAccount(string phoneNumber, decimal money);
    public abstract Task<List<CustomerDB>> GetAllUsers();
}