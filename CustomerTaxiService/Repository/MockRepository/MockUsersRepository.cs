using CustomerTaxiService.Constants;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.General;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockUsersRepository : IUserRepository
{
    private static List<CustomerDB> _mockRepository = new()
    {
        new CustomerDB
        {
            Id = 1,
            Name = "Name",
            LastName = "LastName",
            PhoneNumber = "12345",
            FeedBack = (int)FeedBack.Good,
            Status = 1,
            RegistrationDate = DateTime.Now,
            AvailableMoney = 100
        }
    };

    public async Task<string> AddNewUser(Customer customer)
    {
        var checkResult = await CheckOfExist(customer.PhoneNumber);
        if (checkResult != UserConstants.UserNotFound)
            return checkResult;
        
        _mockRepository.Add(await ConvertUserToDatabase(customer));
        
        return UserConstants.Ok;
    }

    public async Task<Customer?> GetUserByPhoneNumber(string number)
    {
        var userFromDb = _mockRepository.FirstOrDefault(x => x.PhoneNumber == number);
        return userFromDb == null ? null : await ConvertUserFromDatabase(userFromDb);
    }

    public async Task<string> RemoveUser(string phoneNumber)
    {
        var entity = await GetUserByPhoneNumber(phoneNumber);
        if (entity == null)
            return UserConstants.UserNotFound;
        
        _mockRepository.Remove(_mockRepository.First(x => x.PhoneNumber == phoneNumber));

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
        var userEntity = _mockRepository.FirstOrDefault(x => x.PhoneNumber == phoneNumber)!;
        return userEntity == null ? UserConstants.UserNotFound : UserConstants.Ok;
    }

    public async Task<string> UpdateUser(Customer user, string existUserNumber)
    {
        var userEntity = await GetUserByPhoneNumber(existUserNumber);
        if (userEntity == null)
            return UserConstants.UserNotFound;

        var updatedUser = new Customer
        {
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            FeedBack = user.FeedBack,
            Status = user.Status,
            RegistrationDate = user.RegistrationDate,
            AvailableMoney = user.AvailableMoney
        };
        _mockRepository.Remove(await ConvertUserToDatabase(user));
        _mockRepository.Add(await ConvertUserToDatabase(updatedUser));

        return UserConstants.Ok;
    }

    public async Task<string> AddMoneyToAccount(string phoneNumber, decimal money)
    {
        var userEntity = await GetUserByPhoneNumber(phoneNumber);
        if (userEntity == null)
            return UserConstants.UserNotFound;
        userEntity.AvailableMoney += money;

        _mockRepository.Remove(_mockRepository.First(db => db.PhoneNumber == phoneNumber));
        _mockRepository.Add(await ConvertUserToDatabase(userEntity));

        return UserConstants.Ok;
    }

    public async Task<List<CustomerDB>> GetAllUsers()
    {
        return _mockRepository;
    }

    private async Task<Customer?> ConvertUserFromDatabase(CustomerDB customerDb)
    {
        return new Customer
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

    private async Task<CustomerDB> ConvertUserToDatabase(Customer customer)
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