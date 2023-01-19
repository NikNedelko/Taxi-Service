using Entities.CustomerTaxiService.CustomerData;

namespace TaxiService.Repository.Customer.Interfaces;

public interface IUserRepository
{
    public Task<string> AddNewUser(Entities.CustomerTaxiService.CustomerData.Customer customer);
    public Task<string> RemoveUser(string phoneNumber);
    public Task<Entities.CustomerTaxiService.CustomerData.Customer?> GetUserByPhoneNumber(string number);
    public Task<string?> PermissionToRide(string userId);
    public Task<string> CheckOfExist(string phoneNumber);
    public Task<string> UpdateUser(Entities.CustomerTaxiService.CustomerData.Customer user, string existUserId);
    public Task<string> AddMoneyToAccount(string phoneNumber, decimal money);
    public Task<List<CustomerDB>> GetAllUsers();
}