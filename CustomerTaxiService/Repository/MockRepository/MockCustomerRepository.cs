using CustomerTaxiService.Repository.Interfaces;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockCustomerRepository : ICustomerRepository
{
    public bool AddNewOrder(string str)
    {
        throw new NotImplementedException();
    }

    public bool DeclineOrder(string str)
    {
        throw new NotImplementedException();
    }
}