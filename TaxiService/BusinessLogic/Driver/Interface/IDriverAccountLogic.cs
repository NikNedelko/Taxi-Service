using Entities.DriverApi.Driver;
using Entities.General;

namespace CustomerTaxiService.BusinessLogic.Driver.Interface;

public interface IDriverAccountLogic
{
    public Task<Response> AddNewDriver(RegistrationForDriver registrationDriver);
    public Task<Response> DeleteDriver(string phoneNumber);
}