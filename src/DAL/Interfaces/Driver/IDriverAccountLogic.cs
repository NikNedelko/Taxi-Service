using Domain.Entities.DriverApi.DriverData;
using Domain.Entities.General;

namespace DAL.Interfaces.Driver;

public interface IDriverAccountLogic
{
    public Task<Response> AddNewDriver(RegistrationForDriver registrationDriver);
    public Task<Response> DeleteDriver(string phoneNumber);
}