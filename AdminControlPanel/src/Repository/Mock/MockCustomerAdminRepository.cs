using AdminControlPanel.Repository.Interfaces;
using CustomerTaxiService.Repository.Interfaces;
using Database.MockDatabase;
using Entities.CustomerTaxiService.CustomerData;
using Entities.General;

namespace AdminControlPanel.Repository.Mock;

public class MockCustomerAdminRepository : ICustomerAdminRepository
{
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

    async Task<string> IUserRepository.AddNewUser(Customer customer)
    {
        throw new NotImplementedException();
    }

    async Task<string> IUserRepository.RemoveUser(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    async Task<Customer?> IUserRepository.GetUserByPhoneNumber(string number)
    {
        throw new NotImplementedException();
    }

    async Task<string?> IUserRepository.PermissionToRide(string userId)
    {
        throw new NotImplementedException();
    }

    async Task<string> IUserRepository.CheckOfExist(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    async Task<string> IUserRepository.UpdateUser(Customer user, string existUserId)
    {
        throw new NotImplementedException();
    }

    async Task<string> IUserRepository.AddMoneyToAccount(string phoneNumber, decimal money)
    {
        throw new NotImplementedException();
    }

    async Task<List<CustomerDB>> IUserRepository.GetAllUsers()
    {
        throw new NotImplementedException();
    }
}