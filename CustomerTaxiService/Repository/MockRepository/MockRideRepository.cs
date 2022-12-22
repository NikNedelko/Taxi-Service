using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.RideData;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockRideRepository : IRideRepository
{
    private List<Ride> _mockRepository = new(); // RideDB
    public async Task<bool> AddNewOrder(Customer? customer)
    {
        try
        { 
            //_mockRepository.Add(await ConvertUserToDatabase(customer)); // model + id of user
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
           // _mockRepository.Remove(new CustomerDB());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return false;
    }
}