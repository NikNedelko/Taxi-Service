using AdminControlPanel.Repository.Interfaces;
using CustomerTaxiService.Repository.Interfaces;
using Database.MockDatabase;
using Entities.CustomerTaxiService.CustomerData;
using Entities.General;

namespace AdminControlPanel.Repository;

public abstract class MockCustomerAdminRepository : ICustomerAdminRepository
{
    private IUserRepository _customerRepository;

    protected MockCustomerAdminRepository(IUserRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<CustomerDB>> GetAllUserWIthId()
    {
        return MockDatabases.CustomerList;
    }

    public async Task<string> DeleteUserById(int userId)
    {
        MockDatabases.CustomerList.Remove(MockDatabases.CustomerList.FirstOrDefault(x => x.Id == userId));
        return "Ok";
    }

    public async Task<CustomerDB?> GetUserById(int userId)
    {
        return MockDatabases.CustomerList.FirstOrDefault(x => x.Id == userId);
    }

    public async Task<string> ChangeCustomerStatus(string phoneNumber, AccountStatus newAccountStatus)
    {
        var entity = await GetCustomerDbByPhoneNumber(phoneNumber);
        entity.Status = (int)newAccountStatus;
        MockDatabases.CustomerList.Remove(MockDatabases.CustomerList.FirstOrDefault(x => x.PhoneNumber == phoneNumber));
        MockDatabases.CustomerList.Add(entity);
        return "Ok";
    }
    
    private async Task<CustomerDB?> GetCustomerDbByPhoneNumber(string phoneNumber)
    {
        return MockDatabases.CustomerList.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
    }


    public abstract Task<string> AddNewUser(Customer customer);
    public abstract Task<string> RemoveUser(string phoneNumber);
    public abstract Task<Customer?> GetUserByPhoneNumber(string number);
    public abstract Task<string?> PermissionToRide(string userId);
    public abstract Task<string> CheckOfExist(string phoneNumber);
    public abstract Task<string> UpdateUser(Customer user, string existUserId);
    public abstract Task<string> AddMoneyToAccount(string phoneNumber, decimal money);
    public abstract Task<List<CustomerDB>> GetAllUsers();
}