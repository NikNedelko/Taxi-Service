using Domain.Entities.DriverApi.DriverData;
using Domain.Entities.General;

namespace Application.BL.DriverLogic.Interface;

public interface IDriverAccountLogic
{
    public Task<Response> AddNewDriver(RegistrationForDriver registrationDriver);
    public Task<Response> DeleteDriver(string phoneNumber);
}