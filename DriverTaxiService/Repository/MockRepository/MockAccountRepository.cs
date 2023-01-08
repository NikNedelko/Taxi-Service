using DriverTaxiService.Constants;
using DriverTaxiService.Repository.Interfaces;
using Entities.DriverApi;
using Entities.DriverApi.Driver;
using Entities.General;

namespace DriverTaxiService.Repository.MockRepository;

public class MockAccountRepository : IAccountRepository
{
    private static List<DriverDB> _mockRepository = new()
    {
        new DriverDB
        {
            Id = 0,
            Name = "Jacob",
            LastName = "Sally",
            PhoneNumber = "12345",
            DriverLicenseNumber = "EU12345",
            Car = "Ford",
            IsWorking = false,
            DriveClass = 1,
            Status = 2,
            FeedBack = 0,
            RegistrationDate = DateTime.Now,
            Balance = 1000
        }
    };

    public async Task<string> AddNewDriver(RegistrationForDriver registrationForDriver)
    {
        var newDriver = new Driver
        {
            Name = registrationForDriver.Name,
            LastName = registrationForDriver.LastName,
            PhoneNumber = registrationForDriver.PhoneNumber,
            DriverLicenseNumber = registrationForDriver.DriverLicenseNumber,
            Car = registrationForDriver.Car,
            DriveClass = await TakeDriveClassByCar(registrationForDriver.Car),
            Status = AccountStatus.Active,
            FeedBack = FeedBack.Good,
            RegistrationDate = DateTime.Now,
            Balance = 0
        };
        _mockRepository.Add(await ConvertToDatabase(newDriver));
        return AccountConstants.DriverWasAdded;
    }

    public async Task<Driver?> GetDriverByNumber(string phoneNumber)
    {
        var entity = _mockRepository.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        return entity == null ? null : await ConvertFromDatabase(entity);
    }

    public async Task<Driver?> GetDriverByLicense(string licenseNumber)
    {
        var entity = _mockRepository.FirstOrDefault(x => x.DriverLicenseNumber == licenseNumber);
        return entity == null ? null : await ConvertFromDatabase(entity);
    }

    public async Task<string> UpdateDriver(Driver newDriver, string phoneNumber)
    {
        var oldEntity = await GetDriverByNumber(phoneNumber);
        _ = await DeleteDriver(oldEntity.PhoneNumber);
        _mockRepository.Add(await ConvertToDatabase(newDriver));
        return AccountConstants.Ok;
    }

    public async Task<string> DeleteDriver(string phoneNumber)
    {
        _mockRepository.Remove(_mockRepository.FirstOrDefault(x => x.PhoneNumber == phoneNumber)!);
        return AccountConstants.DriverWasDeleted;
    }

    public async Task<List<DriverDB>> GetAllDrivers()
    {
        return _mockRepository;
    }

    private async Task<DriveClass> TakeDriveClassByCar(string carName)
    {
        return carName switch
        {
            nameof(CarTypes.Ford) => DriveClass.Economic,
            nameof(CarTypes.Toyota) => DriveClass.Medium,
            nameof(CarTypes.Mercedes) => DriveClass.Premium,

            _ => throw new ArgumentOutOfRangeException(nameof(carName), carName, "Unsupported car")
        };
    }

    private async Task<DriverDB> ConvertToDatabase(Driver driver)
    {
        return new DriverDB
        {
            Id = new Random().Next(1,99),
            Name = driver.Name,
            LastName = driver.LastName,
            PhoneNumber = driver.PhoneNumber,
            DriverLicenseNumber = driver.DriverLicenseNumber,
            Car = driver.Car,
            IsWorking = driver.IsWorking,
            DriveClass = (int)driver.DriveClass,
            Status = (int)driver.Status,
            FeedBack = (int)driver.FeedBack,
            RegistrationDate = driver.RegistrationDate,
            Balance = driver.Balance
        };
    }

    private async Task<Driver> ConvertFromDatabase(DriverDB driverDb)
    {
        return new Driver
        {
            
            Name = driverDb.Name,
            LastName = driverDb.LastName,
            PhoneNumber = driverDb.PhoneNumber,
            DriverLicenseNumber = driverDb.DriverLicenseNumber,
            Car = driverDb.Car,
            IsWorking = driverDb.IsWorking,
            DriveClass = (DriveClass)driverDb.DriveClass,
            Status = (AccountStatus)driverDb.Status,
            FeedBack = (FeedBack)driverDb.FeedBack,
            RegistrationDate = driverDb.RegistrationDate,
            Balance = driverDb.Balance
        };
    }
}