using Entities.CustomerTaxiService.CustomerData;

namespace CustomerTaxiService.Repository.Interfaces;

public interface IUserRepository
{
    public Task<string> AddNewUser(Customer customer);
    public Task<string> RemoveUser(string name);
    public Task<Customer?> GetUserByPhoneNumber(string number);
    public Task<string?> PermissionToRide(string name);
    public Task<string> CheckOfExist(string name);
    public Task<string> UpdateUser(string name);
}