using Entities.CustomerApi.CustomerData;

namespace TaxiService.Repository.Customer.Interfaces;

public interface IUserRepository
{
    public Task<string> AddNewUser(Entities.CustomerApi.CustomerData.Customer customer);
    public Task<string> RemoveUser(string phoneNumber);
    public Task<Entities.CustomerApi.CustomerData.Customer?> GetUserByPhoneNumber(string number);
    public Task<string?> PermissionToRide(string userId);
    public Task<string> CheckOfExist(string phoneNumber);
    public Task<string> UpdateUser(Entities.CustomerApi.CustomerData.Customer user, string existUserId);
    public Task<string> AddMoneyToAccount(string phoneNumber, decimal money);
    public Task<List<CustomerDB>> GetAllUsers();
}