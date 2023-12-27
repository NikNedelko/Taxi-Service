using Domain.Entities.CustomerData;

namespace DAL.Repository.Interfaces.CustomerRepository;

public interface IUserRepository
{
    public Task<string> AddNewUser(CustomerModel customer);
    public Task<string> RemoveUser(string phoneNumber);
    public Task<CustomerModel?> GetUserByPhoneNumber(string number);
    public Task<string?> PermissionToRide(string phoneNumber);
    public Task<string> CheckOfExist(string phoneNumber);
    public Task<string> UpdateUser(CustomerModel user, string existUserNumber);
    public Task<string> AddMoneyToAccount(string phoneNumber, decimal money);
    public Task<List<CustomerDB>> GetAllUsers();
}