using Database.MockDatabase;
using Entities.CustomerTaxiService.CustomerData;
using Entities.General;
using TaxiService.Constants.Customer.Account;
using TaxiService.Repository.Customer.Interfaces;

namespace TaxiService.Repository.Customer.MockRepository;

public class MockUsersRepository : IUserRepository
{
    public async Task<string> AddNewUser(Entities.CustomerTaxiService.CustomerData.Customer customer)
    {
        var checkResult = await CheckOfExist(customer.PhoneNumber);
        if (checkResult != UserConstants.UserNotFound)
            return checkResult;

        MockDatabases.CustomerList.Add(await ConvertUserToDatabase(customer));

        return UserConstants.Ok;
    }

    public async Task<Entities.CustomerTaxiService.CustomerData.Customer?> GetUserByPhoneNumber(string number)
    {
        var userFromDb = MockDatabases.CustomerList.FirstOrDefault(x => x.PhoneNumber == number);
        return userFromDb == null ? null : await ConvertUserFromDatabase(userFromDb);
    }

    public async Task<string> RemoveUser(string phoneNumber)
    {
        var entity = await GetUserByPhoneNumber(phoneNumber);
        if (entity == null)
            return UserConstants.UserNotFound;

        MockDatabases.CustomerList.Remove(MockDatabases.CustomerList.First(x => x.PhoneNumber == phoneNumber));

        return UserConstants.UserWasDeleted;
    }

    public async Task<string?> PermissionToRide(string phoneNumber)
    {
        var userEntityDb = await GetUserByPhoneNumber(phoneNumber);
        if (userEntityDb == null)
            return null;
        return userEntityDb.Status == AccountStatus.Active
            ? UserConstants.Ok
            : UserConstants.UserDoesntHavePermissionToRide;
    }

    public async Task<string> CheckOfExist(string phoneNumber)
    {
        var userEntity = MockDatabases.CustomerList.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        return userEntity == null ? UserConstants.UserNotFound : UserConstants.Ok;
    }

    public async Task<string> UpdateUser(Entities.CustomerTaxiService.CustomerData.Customer user, string existUserNumber)
    {
        var userEntity = await GetUserByPhoneNumber(existUserNumber);
        if (userEntity == null)
            return UserConstants.UserNotFound;

        var updatedUser = new Entities.CustomerTaxiService.CustomerData.Customer
        {
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            FeedBack = user.FeedBack,
            Status = user.Status,
            RegistrationDate = user.RegistrationDate,
            AvailableMoney = user.AvailableMoney
        };
        MockDatabases.CustomerList.Remove(await ConvertUserToDatabase(user));

        MockDatabases.CustomerList.Add(await ConvertUserToDatabase(updatedUser));

        return UserConstants.Ok;
    }

    public async Task<string> AddMoneyToAccount(string phoneNumber, decimal money)
    {
        var userEntity = await GetUserByPhoneNumber(phoneNumber);
        if (userEntity == null)
            return UserConstants.UserNotFound;
        userEntity.AvailableMoney += money;

        MockDatabases.CustomerList.Remove(MockDatabases.CustomerList.First(db => db.PhoneNumber == phoneNumber));
        MockDatabases.CustomerList.Add(await ConvertUserToDatabase(userEntity));

        return UserConstants.Ok;
    }

    public async Task<List<CustomerDB>> GetAllUsers()
    {
        return MockDatabases.CustomerList;
    }

    private async Task<Entities.CustomerTaxiService.CustomerData.Customer?> ConvertUserFromDatabase(CustomerDB customerDb)
    {
        return new Entities.CustomerTaxiService.CustomerData.Customer
        {
            Name = customerDb.Name,
            LastName = customerDb.LastName,
            PhoneNumber = customerDb.PhoneNumber,
            FeedBack = (FeedBack)customerDb.FeedBack,
            Status = AccountStatus.NoData,
            RegistrationDate = customerDb.RegistrationDate,
            AvailableMoney = customerDb.AvailableMoney
        };
    }

    private async Task<CustomerDB> ConvertUserToDatabase(Entities.CustomerTaxiService.CustomerData.Customer customer)
    {
        return new CustomerDB
        {
            Name = customer.Name,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            FeedBack = Convert.ToInt32(customer.FeedBack),
            Status = Convert.ToInt32(customer.Status),
            AvailableMoney = customer.AvailableMoney
        };
    }
}