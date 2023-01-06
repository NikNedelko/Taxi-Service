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
        _mockRepository.Add( await ConvertToDatabase(newDriver));
        return AccountConstants.DriverWasAdded;
    }

    public Task<Driver?> GetDriverByNumber(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task<Driver?> GetDriverByLicense(string licenseNumber)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateDriver()
    {
        throw new NotImplementedException();
    }

    public async Task<string> DeleteDriver(string phoneNumber)
    {
        var driverEntity = await GetDriverByNumber(phoneNumber);
        _mockRepository.Remove(_mockRepository.FirstOrDefault(x => x.PhoneNumber == phoneNumber)!);
        return AccountConstants.DriverWasAdded;
    }

    public Task<List<DriverDB>> GetAllDrivers()
    {
        throw new NotImplementedException();
    }

    private async Task<DriveClass> TakeDriveClassByCar(string carName)
    {
        return carName switch
        {
            nameof(CarTypes.Ford)=> DriveClass.Economic,
            nameof(CarTypes.Toyota)=> DriveClass.Medium,
            nameof(CarTypes.Mercedes)=> DriveClass.Premium,

            _ => throw new ArgumentOutOfRangeException(nameof(carName), carName, "Unsupported car")
        };
    }

    private async Task<DriverDB> ConvertToDatabase(Driver driver)
    {
        return  new DriverDB
        {
            Name = driver.Name,
            LastName = driver.LastName,
            PhoneNumber = driver.PhoneNumber,
            DriverLicenseNumber = driver.DriverLicenseNumber,
            Car = driver.Car,
            IsWorking = false,
            DriveClass = (int)driver.DriveClass,
            Status = (int)driver.Status,
            FeedBack = (int)driver.FeedBack,
            RegistrationDate = driver.RegistrationDate,
            Balance = driver.Balance
        };
    }
    
    private async Task<DriverDB> ConvertFromDatabase(DriverDB driverDb)
    {
        return  new DriverDB
        {
            Name = driverDb.Name,
            LastName = driverDb.LastName,
            PhoneNumber = driverDb.PhoneNumber,
            DriverLicenseNumber = driverDb.DriverLicenseNumber,
            Car = driverDb.Car,
            IsWorking = driverDb.IsWorking,
            DriveClass = driverDb.DriveClass,
            Status = driverDb.Status,
            FeedBack = driverDb.FeedBack,
            RegistrationDate = driverDb.RegistrationDate,
            Balance = driverDb.Balance
        };
    }
}