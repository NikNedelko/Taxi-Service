using Database.MockDatabase;
using Entities.DriverApi.DriverData;
using Entities.General;
using TaxiService.Constants.DriverConstants;
using TaxiService.Repository.DriverRepository.Interfaces;

namespace TaxiService.Repository.DriverRepository.MockRepository;

public class MockDriverAccountRepository : IDriverAccountRepository
{
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
        MockDatabases.DriverList.Add(await ConvertToDatabase(newDriver));
        return DriverConstants.DriverWasAdded;
    }

    public async Task<Driver?> GetDriverByNumber(string phoneNumber)
    {
        var entity = MockDatabases.DriverList.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        return entity == null ? null : await ConvertFromDatabase(entity);
    }

    public async Task<Driver?> GetDriverByLicense(string licenseNumber)
    {
        var entity = MockDatabases.DriverList.FirstOrDefault(x => x.DriverLicenseNumber == licenseNumber);
        return entity == null ? null : await ConvertFromDatabase(entity);
    }

    public async Task<string> UpdateDriver(Driver newDriver, string phoneNumber)
    {
        var oldEntity = await GetDriverByNumber(phoneNumber);
        _ = await DeleteDriver(oldEntity.PhoneNumber);
        MockDatabases.DriverList.Add(await ConvertToDatabase(newDriver));
        return DriverConstants.Ok;
    }

    public async Task<string> DeleteDriver(string phoneNumber)
    {
        MockDatabases.DriverList.Remove(MockDatabases.DriverList.FirstOrDefault(x => x.PhoneNumber == phoneNumber)!);
        return DriverConstants.DriverWasDeleted;
    }

    private async Task<List<DriverDb>> GetAllDriversWithId()
    {
        return MockDatabases.DriverList;
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

    private async Task<DriverDb> ConvertToDatabase(Driver driver)
    {
        return new DriverDb
        {
            Id = new Random().Next(1, 99),
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

    private async Task<Driver> ConvertFromDatabase(DriverDb driverDb)
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