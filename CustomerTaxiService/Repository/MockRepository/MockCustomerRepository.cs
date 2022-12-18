using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockCustomerRepository : ICustomerRepository
{
    private List<CustomerDB> MockRepository = new(); // CustomerDB
    public async Task<string> AddNewOrder(Customer customer)
    {
        try
        {
            //convert*
            MockRepository.Add(new CustomerDB()); // model + id of user
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return "";
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
}