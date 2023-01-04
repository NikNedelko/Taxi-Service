using DriverTaxiService.Repository.Interfaces;

namespace DriverTaxiService.Repository.MockRepository;

public class MockDriveRepository : IDriveRepository
{
    public Task<string> StartWork()
    {
        throw new NotImplementedException();
    }

    public Task<string> EndWork()
    {
        throw new NotImplementedException();
    }
}