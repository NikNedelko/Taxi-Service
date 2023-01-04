using DriverTaxiService.Repository.Interfaces;

namespace DriverTaxiService.Repository.MockRepository;

public class MockAccountRepository : IAccountRepository
{
    public Task<string> AddNewDriver()
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateDriver()
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteDriver()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAllDrivers()
    {
        throw new NotImplementedException();
    }
}