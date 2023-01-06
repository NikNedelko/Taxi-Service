using DriverTaxiService.Repository.Interfaces;

namespace DriverTaxiService.Repository.MockRepository;

public class MockDriveRepository : IDriveRepository
{
    public Task<string> StartWork(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task<string> EndWork(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAllAvailableOrders(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task<string> TakeOrderById(string phoneNumber)
    {
        throw new NotImplementedException();
    }
}