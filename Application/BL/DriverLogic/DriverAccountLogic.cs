using Entities.DriverApi.DriverData;
using Entities.General;
using TaxiService.BusinessLogic.DriverLogic.Interface;
using TaxiService.BusinessLogic.General;
using TaxiService.Constants.DriverConstants;
using TaxiService.Repository.DriverRepository.Interfaces;

namespace TaxiService.BusinessLogic.DriverLogic;

public class DriverAccountLogic : IDriverAccountLogic
{
    private readonly IDriverAccountRepository _accountRepository;

    public DriverAccountLogic(IDriverAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Response> AddNewDriver(RegistrationForDriver registrationDriver)
    {
        var checkNumber = await CheckDriverByPhoneNumber(registrationDriver.PhoneNumber);
        if (checkNumber == DriverConstants.DriverIsExist)
            return await GeneralMethods.CreateResponse(DriverConstants.DriverIsExist);

        var checkLicense = await CheckDriverByLicenseNumber(registrationDriver.DriverLicenseNumber);
        if (checkLicense == DriverConstants.Ok)
            return await GeneralMethods.CreateResponse(DriverConstants.DriverIsExist);

        var addResult = await AddNewDriverToDatabase(registrationDriver);
        return await GeneralMethods.CreateResponse(addResult);
    }

    public async Task<Response> DeleteDriver(string phoneNumber)
    {
        var checkNumber = await CheckDriverByPhoneNumber(phoneNumber);
        if (checkNumber == DriverConstants.DriverIsNotExist)
            return await GeneralMethods.CreateResponse(DriverConstants.DriverIsExist);

        return await GeneralMethods.CreateResponse(await _accountRepository.DeleteDriver(phoneNumber));
    }

    private async Task<string> AddNewDriverToDatabase(RegistrationForDriver registrationForDriver)
    {
        return await _accountRepository.AddNewDriver(registrationForDriver);
    }

    private async Task<string> CheckDriverByPhoneNumber(string phoneNumber)
    {
        var driverEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        return driverEntity == null ? DriverConstants.DriverIsNotExist : DriverConstants.DriverIsExist;
    }

    private async Task<string> CheckDriverByLicenseNumber(string licenseNumber)
    {
        var driverEntity = await _accountRepository.GetDriverByLicense(licenseNumber);
        return driverEntity == null ? DriverConstants.DriverIsNotExist : DriverConstants.Ok;
    }
}