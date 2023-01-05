using Entities.DriverApi.Driver;
using Entities.General;

namespace DriverTaxiService.BusinessLogic.Interface;

public interface IAccountLogic
{
    public Task<Response> AddNewDriver(RegistrationForDriver registrationDriver);
    public Task<Response> DeleteDriver(string phoneNumber);
}