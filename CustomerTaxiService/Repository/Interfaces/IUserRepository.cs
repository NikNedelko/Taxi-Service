using Entities.CustomerTaxiService.CustomerData;

namespace CustomerTaxiService.Repository.Interfaces;

public interface IUserRepository
{
    public Task<string> AddNewUser(Customer customer);
    public Task<string> RemoveUser(Customer userEntity);
    public Task<Customer?> GetUserByPhoneNumber(string number);
    public Task<string?> PermissionToRide(string userId);
    public Task<string> CheckOfExist(string phoneNumber);
    public Task<Customer?> UpdateUser(Customer user, string existUserId);
}