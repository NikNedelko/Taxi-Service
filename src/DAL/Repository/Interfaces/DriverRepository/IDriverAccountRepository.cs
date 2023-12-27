using Domain.Entities.DriverData;

namespace DAL.Repository.Interfaces.DriverRepository;

public interface IDriverAccountRepository
{
    public Task<string> AddNewDriver(RegistrationForDriver registrationForDriver);
    public Task<DriverModel?> GetDriverByNumber(string phoneNumber);
    public Task<DriverModel?> GetDriverByLicense(string licenseNumber);
    public Task<string> UpdateDriver(DriverModel newDriver, string phoneNumber);
    public Task<string> DeleteDriver(string phoneNumber);
}