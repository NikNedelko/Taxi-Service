using Entities.DriverApi.DriverData;
using Entities.General;

namespace TaxiService.BusinessLogic.DriverLogic.Interface;

public interface IDriverAccountLogic
{
    public Task<Response> AddNewDriver(RegistrationForDriver registrationDriver);
    public Task<Response> DeleteDriver(string phoneNumber);
}