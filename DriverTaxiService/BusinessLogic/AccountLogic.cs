using DriverTaxiService.BusinessLogic.Interface;
using Entities.DriverApi.Driver;

namespace DriverTaxiService.BusinessLogic;

public class AccountLogic : IAccountLogic
{
    public async Task<string> AddNewDriver(RegistrationForDriver registrationDriver)
    {
        throw new NotImplementedException();
    }

    public async Task<string> DeleteDriver(string phoneNumber)
    {
        throw new NotImplementedException();
    }
}