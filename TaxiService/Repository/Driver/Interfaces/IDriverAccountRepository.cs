using Entities.DriverApi.Driver;

namespace TaxiService.Repository.Driver.Interfaces;

public interface IDriverAccountRepository
{
    public Task<string> AddNewDriver(RegistrationForDriver registrationForDriver);
    public Task<Entities.DriverApi.Driver.Driver?> GetDriverByNumber(string phoneNumber);
    public Task<Entities.DriverApi.Driver.Driver?> GetDriverByLicense(string licenseNumber);
    public Task<string> UpdateDriver(Entities.DriverApi.Driver.Driver newDriver, string phoneNumber);
    public Task<string> DeleteDriver(string phoneNumber);
}