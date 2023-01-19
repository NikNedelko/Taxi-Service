using AdminControlPanel.Repository.Interfaces;
using Database.MockDatabase;
using Entities.DriverApi.Driver;
using TaxiService.Repository.Driver.Interfaces;

namespace AdminControlPanel.Repository;

public class MockDriverAdminRepository : IDriverAdminRepository
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

    async Task<string> IDriverAccountRepository.AddNewDriver(RegistrationForDriver registrationForDriver)
    {
        throw new NotImplementedException();
    }

    async Task<Driver?> IDriverAccountRepository.GetDriverByNumber(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    async Task<Driver?> IDriverAccountRepository.GetDriverByLicense(string licenseNumber)
    {
        throw new NotImplementedException();
    }

    async Task<string> IDriverAccountRepository.UpdateDriver(Driver newDriver, string phoneNumber)
    {
        throw new NotImplementedException();
    }

    async Task<string> IDriverAccountRepository.DeleteDriver(string phoneNumber)
    {
        throw new NotImplementedException();
    }
}