using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockCustomerRepository : IRideRepository
{
    private List<CustomerDB> MockRepository = new(); // CustomerDB
    public async Task<bool> AddNewOrder(Customer customer)
    {
        try
        { 
            MockRepository.Add(await ConvertUserToDatabase(customer)); // model + id of user
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    public async Task<bool> CancelOrder(string str)
    {
        try
        { 
            MockRepository.Remove(new CustomerDB());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return false;
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