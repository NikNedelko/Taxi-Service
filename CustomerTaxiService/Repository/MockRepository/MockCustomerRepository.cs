using CustomerTaxiService.Repository.Interfaces;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockCustomerRepository : ICustomerRepository
{
    private List<string> MockRepository = new();
    public async Task<bool> AddNewOrder(string str)
    {
        try
        {
            MockRepository.Add(str);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return false;
    }

    public async Task<bool> CancelOrder(string str)
    {
        try
        {
            MockRepository.Remove(str);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return false;
    }
}