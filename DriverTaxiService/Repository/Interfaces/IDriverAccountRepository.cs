using Entities.DriverApi.Driver;

namespace DriverTaxiService.Repository.Interfaces;

public interface IDriverAccountRepository
{
    public Task<string> AddNewDriver(RegistrationForDriver registrationForDriver);
    public Task<Driver?> GetDriverByNumber(string phoneNumber);
    public Task<Driver?> GetDriverByLicense(string licenseNumber);
    public Task<string> UpdateDriver(Driver newDriver, string phoneNumber);
    public Task<string> DeleteDriver(string phoneNumber);
    public Task<List<DriverDB>> GetAllDrivers();
}