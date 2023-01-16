using AdminControlPanel.Repository.Interfaces;
using Database.MockDatabase;
using Entities.DriverApi.Driver;

namespace AdminControlPanel.Repository;

public abstract class MockDriverAdminRepository : IDriverAdminRepository
{
    public async Task<List<DriverDB>> GetAllDriversWithId()
    {
        return MockDatabases.DriverList;
    }

    public async Task<DriverDB?> GetDriverById(int id)
    {
        return MockDatabases.DriverList.FirstOrDefault(x => x.Id == id);
    }

    public async Task<string> DeleteDriverById(int id)
    {
        MockDatabases.DriverList.Remove(MockDatabases.DriverList.FirstOrDefault(x => x.Id == id));
        return "Ok";
    }

    public abstract Task<string> AddNewDriver(RegistrationForDriver registrationForDriver);
    public abstract Task<Driver?> GetDriverByNumber(string phoneNumber);
    public abstract Task<Driver?> GetDriverByLicense(string licenseNumber);
    public abstract Task<string> UpdateDriver(Driver newDriver, string phoneNumber);
    public abstract Task<string> DeleteDriver(string phoneNumber);
}