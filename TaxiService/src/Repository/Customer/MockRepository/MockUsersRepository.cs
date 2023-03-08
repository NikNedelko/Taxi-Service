using Database.MockDatabase;
using Entities.CustomerApi.CustomerData;
using Entities.General;
using TaxiService.Constants.Customer;
using TaxiService.Repository.Customer.Interfaces;

namespace TaxiService.Repository.Customer.MockRepository;

public class MockUsersRepository : IUserRepository
{
    public async Task<string> AddNewUser(Entities.CustomerApi.CustomerData.Customer customer)
    {
        var checkResult = await CheckOfExist(customer.PhoneNumber);
        if (checkResult != CustomerConstants.UserNotFound)
            return checkResult;

        MockDatabases.CustomerList.Add(await ConvertUserToDatabase(customer, null));

        return CustomerConstants.Ok;
    }

    public async Task<Entities.CustomerApi.CustomerData.Customer?> GetUserByPhoneNumber(string number)
    {
        var userFromDb = MockDatabases.CustomerList.FirstOrDefault(x => x.PhoneNumber == number);
        return userFromDb == null ? null : await ConvertUserFromDatabase(userFromDb);
    }

    public async Task<string> RemoveUser(string phoneNumber)
    {
        var entity = await GetUserByPhoneNumber(phoneNumber);
        if (entity == null)
            return CustomerConstants.UserNotFound;

        MockDatabases.CustomerList.Remove(MockDatabases.CustomerList.First(x => x.PhoneNumber == phoneNumber));

        return CustomerConstants.UserWasDeleted;
    }

    public async Task<string?> PermissionToRide(string phoneNumber)
    {
        var userEntityDb = await GetUserByPhoneNumber(phoneNumber);
        if (userEntityDb == null)
            return null;
        return userEntityDb.Status == AccountStatus.Active
            ? CustomerConstants.Ok
            : CustomerConstants.UserDoesntHavePermissionToRide;
    }

    public async Task<string> CheckOfExist(string phoneNumber)
    {
        var userEntity = MockDatabases.CustomerList.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        return userEntity == null ? CustomerConstants.UserNotFound : CustomerConstants.Ok;
    }

    public async Task<string> UpdateUser(Entities.CustomerApi.CustomerData.Customer user, string existUserNumber)
    {
        var userEntity = await GetUserByPhoneNumber(existUserNumber);
        if (userEntity == null)
            return CustomerConstants.UserNotFound;

        var updatedUser = new Entities.CustomerApi.CustomerData.Customer
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            FeedBack = user.FeedBack,
            Status = user.Status,
            RegistrationDate = user.RegistrationDate,
            AvailableMoney = user.AvailableMoney
        };
        var idForUpdate = MockDatabases.CustomerList.FirstOrDefault(x => x.PhoneNumber == existUserNumber)!.Id;
        MockDatabases.CustomerList.Remove(MockDatabases.CustomerList.FirstOrDefault(x=>x.PhoneNumber == existUserNumber));

        MockDatabases.CustomerList.Add(await ConvertUserToDatabase(updatedUser,idForUpdate));

        return CustomerConstants.Ok;
    }

    public async Task<string> AddMoneyToAccount(string phoneNumber, decimal money)
    {
        var userEntity = await GetUserByPhoneNumber(phoneNumber);
        if (userEntity == null)
            return CustomerConstants.UserNotFound;
        userEntity.AvailableMoney += money;
        var idForUpdate = MockDatabases.CustomerList.FirstOrDefault(x => x.PhoneNumber == phoneNumber)!.Id;
        MockDatabases.CustomerList.Remove(MockDatabases.CustomerList.First(db => db.PhoneNumber == phoneNumber));

        MockDatabases.CustomerList.Add(await ConvertUserToDatabase(userEntity,idForUpdate));

        return CustomerConstants.Ok;
    }

    public async Task<List<CustomerDB>> GetAllUsers()
    {
        return MockDatabases.CustomerList;
    }

    private async Task<Entities.CustomerApi.CustomerData.Customer?> ConvertUserFromDatabase(CustomerDB customerDb)
    {
        return new Entities.CustomerApi.CustomerData.Customer
        {
            Name = customerDb.Name,
            LastName = customerDb.LastName,
            PhoneNumber = customerDb.PhoneNumber,
            Email = customerDb.Email,
            FeedBack = (FeedBack)customerDb.FeedBack,
            Status = AccountStatus.NoData,
            RegistrationDate = customerDb.RegistrationDate,
            AvailableMoney = customerDb.AvailableMoney
        };
    }

    private async Task<CustomerDB> ConvertUserToDatabase(Entities.CustomerApi.CustomerData.Customer customer, int? Id)
    {
        return new CustomerDB
        {
            Id = Id ?? default,
            Name = customer.Name,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            FeedBack = Convert.ToInt32(customer.FeedBack),
            Status = Convert.ToInt32(customer.Status),
            AvailableMoney = customer.AvailableMoney,
            RegistrationDate = customer.RegistrationDate
        };
    }
}