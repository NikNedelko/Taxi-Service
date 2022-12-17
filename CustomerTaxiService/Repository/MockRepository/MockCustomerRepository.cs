using CustomerTaxiService.Repository.Interfaces;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockCustomerRepository : ICustomerRepository
{
    private List<string> MockRepository = new(); // CustomerDB
    public async Task<string> AddNewOrder(string str)
    {
        try
        {
            //convert*
            MockRepository.Add(str);
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