using Entities.DriverApi.Driver;
using Entities.General;
using TaxiService.BusinessLogic.Driver.Interface;
using TaxiService.BusinessLogic.General;
using TaxiService.Constants.Driver.AccountConstants;
using TaxiService.Repository.Driver.Interfaces;

namespace TaxiService.BusinessLogic.Driver;

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
        if (checkNumber == AccountConstants.DriverIsExist)
            return await GeneralMethods.CreateResponse(AccountConstants.DriverIsExist);
        
        var checkLicense = await CheckDriverByLicenseNumber(registrationDriver.DriverLicenseNumber);
        if (checkLicense == AccountConstants.Ok)
            return await GeneralMethods.CreateResponse(AccountConstants.DriverIsExist);
        
        var addResult = await AddNewDriverToDatabase(registrationDriver);
        return await GeneralMethods.CreateResponse(addResult);
    }

    public async Task<Response> DeleteDriver(string phoneNumber)
    {
        var checkNumber = await CheckDriverByPhoneNumber(phoneNumber);
        if (checkNumber == AccountConstants.DriverIsNotExist)
            return await GeneralMethods.CreateResponse(AccountConstants.DriverIsExist);
        
        return await GeneralMethods.CreateResponse(await _accountRepository.DeleteDriver(phoneNumber));
    }

    private async Task<string> AddNewDriverToDatabase(RegistrationForDriver registrationForDriver)
    {
        return await _accountRepository.AddNewDriver(registrationForDriver);
    }

    private async Task<string> CheckDriverByPhoneNumber(string phoneNumber)
    {
        var driverEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        return driverEntity == null ? AccountConstants.DriverIsNotExist : AccountConstants.DriverIsExist;
    }
    
    private async Task<string> CheckDriverByLicenseNumber(string licenseNumber)
    {
        var driverEntity = await _accountRepository.GetDriverByLicense(licenseNumber);
        return driverEntity == null ? AccountConstants.DriverIsNotExist : AccountConstants.Ok;
    }
}