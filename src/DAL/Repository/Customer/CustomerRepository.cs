using DAL.Database.Context;
using DAL.Repository.Interfaces.CustomerRepository;
using Domain.Entities.CustomerData;
using Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using TaxiService.Constants.Customer;

namespace DAL.Repository.Customer;

public class CustomerRepository : IUserRepository
{
    private readonly DatabaseContext _database;

    public CustomerRepository(DatabaseContext database)
    {
        _database = database;
    }

    public async Task<string> AddNewUser(CustomerModel customer)
    {
        var checkResult = await CheckOfExist(customer.PhoneNumber);
        if (checkResult != CustomerConstants.UserNotFound)
            return checkResult;

        await _database.Customers.AddAsync(await ConvertUserToDatabase(customer, null));

        return CustomerConstants.Ok;
    }

    public async Task<string> RemoveUser(string phoneNumber)
    {
        var entity = await GetDbUserByPhoneNumber(phoneNumber);
        if (entity == null)
            return CustomerConstants.UserNotFound;

        _database.Customers.Remove(entity);

        return CustomerConstants.Ok;
    }

    public async Task<CustomerModel?> GetUserByPhoneNumber(string number)
    {
        var user = await _database.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == number);
        return user == null ? null : await ConvertUserFromDatabase(user);
    }

    private async Task<CustomerDB?> GetDbUserByPhoneNumber(string number)
    {
        return await _database.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == number);
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
        var userEntity = _database.Customers.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        return userEntity == null ? CustomerConstants.UserNotFound : CustomerConstants.Ok;
    }

    public async Task<string> UpdateUser(CustomerModel user, string existUserNumber)
    {
        var oldData = await _database.Customers
            .FirstOrDefaultAsync(x => x.PhoneNumber == existUserNumber);
        if (oldData == null)
            return CustomerConstants.UserNotFound;
        
        oldData.AvailableMoney = user.AvailableMoney;
        oldData.RegistrationDate = user.RegistrationDate;
        oldData.PhoneNumber = user.PhoneNumber;
        oldData.FeedBack = (int)user.FeedBack;
        oldData.Email = user.Email;
        oldData.Name = user.Name;
        oldData.Status = (int)user.Status;
        oldData.LastName = user.LastName;

        await  _database.SaveChangesAsync();
        return CustomerConstants.Ok;
    }

    public async Task<string> AddMoneyToAccount(string phoneNumber, decimal money)
    {
        var oldData = await _database.Customers
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        if (oldData == null)
            return CustomerConstants.UserNotFound;

        oldData.AvailableMoney += money;
        
        await  _database.SaveChangesAsync();
        return CustomerConstants.Ok;
    }

    public async Task<List<CustomerDB>> GetAllUsers()
    {
        var listOfUsers = await _database.Customers.ToListAsync();
        return listOfUsers;
    }

    private async Task<CustomerDB> ConvertUserToDatabase(CustomerModel customer, int? Id)
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
    
    private async Task<CustomerModel?> ConvertUserFromDatabase(CustomerDB customerDb)
    {
        return new CustomerModel
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
}