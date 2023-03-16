using AdminControlPanel.Repository.Interfaces;
using Database.MockDatabase;
using Entities.DriverApi.DriverData;
using TaxiService.Repository.DriverRepository.Interfaces;

namespace AdminControlPanel.Repository.Mock;

public class MockDriverAdminRepository : IDriverAdminRepository
{
    public async Task<List<DriverDb>> GetAllDriversWithId()
    {
        return MockDatabases.DriverList;
    }

    public async Task<DriverDb?> GetDriverById(int id)
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