using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.General;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockUsersRepository : IUserRepository
{
    private List<CustomerDB> MockRepository = new();

    public async Task<string> AddNewUser(Customer customer)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Customer> GetUserByPhoneNumber(string number)
    {
        var userFromDb = new CustomerDB();
        try
        {
             userFromDb = MockRepository.FirstOrDefault(x => x.PhoneNumber == number);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return userFromDb == null ? null : await ConvertUserFromDatabase(userFromDb);
    }

    public async Task<string> RemoveUser(string name)
    {
        throw new NotImplementedException();
    }

    public Task<string?> PermissionToRide(string name)
    {
        throw new NotImplementedException();
    }


    public async Task<string> CheckOfExist(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UpdateUser(string name)
    {
        throw new NotImplementedException();
    }

    private async Task<Customer> ConvertUserFromDatabase(CustomerDB customerDb)
    {
        return new Customer
        {
            id = customerDb.Id,
            Name = customerDb.Name,
            LastName = customerDb.LastName,
            PhoneNumber = customerDb.PhoneNumber,
            FeedBack = (FeedBack)customerDb.FeedBack,
            Status = AccountStatus.NoData,
            RegistrationDate = customerDb.RegistrationDate,
            AvailableMoney = customerDb.AvailableMoney,

        };
    }
    
    private async Task<CustomerDB> ConvertUserToDatabase(Customer customer)
    {
        return new CustomerDB
        {
            Id = customer.id,
            Name = customer.Name,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            FeedBack = Convert.ToInt32(customer.FeedBack),
            Status = Convert.ToInt32(customer.Status),
            AvailableMoney = customer.AvailableMoney
        };
    }
}