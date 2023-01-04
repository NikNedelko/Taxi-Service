using Entities.DriverApi.Driver;

namespace DriverTaxiService.BusinessLogic.Interface;

public interface IAccountLogic
{
    public Task<string> AddNewDriver(RegistrationForDriver registrationDriver);
    public Task<string> DeleteDriver(string phoneNumber);
}