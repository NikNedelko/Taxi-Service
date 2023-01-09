using DriverTaxiService.BusinessLogic.Interface;
using DriverTaxiService.Constants;
using DriverTaxiService.Repository.Interfaces;
using Entities.DriverApi.Driver;
using Entities.General;

namespace DriverTaxiService.BusinessLogic;

public class AccountLogic : IAccountLogic
{
    private readonly IAccountRepository _accountRepository;

    public AccountLogic(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Response> AddNewDriver(RegistrationForDriver registrationDriver)
    {
        var checkNumber = await CheckDriverByPhoneNumber(registrationDriver.PhoneNumber);
        if (checkNumber == AccountConstants.DriverIsExist)
            return await CreateResponse(AccountConstants.DriverIsExist);
        
        var checkLicense = await CheckDriverByLicenseNumber(registrationDriver.DriverLicenseNumber);
        if (checkLicense == AccountConstants.Ok)
            return await CreateResponse(AccountConstants.DriverIsExist);
        
        var addResult = await AddNewDriverToDatabase(registrationDriver);
        return await CreateResponse(addResult);
    }

    public async Task<Response> DeleteDriver(string phoneNumber)
    {
        var checkNumber = await CheckDriverByPhoneNumber(phoneNumber);
        if (checkNumber == AccountConstants.DriverIsNotExist)
            return await CreateResponse(AccountConstants.DriverIsExist);
        
        return await CreateResponse(await _accountRepository.DeleteDriver(phoneNumber));
    }

    public async Task<List<DriverDB>> GetAllDrivers()
    {
        return await _accountRepository.GetAllDrivers();
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
    
    private async Task<Response> CreateResponse(string message)
        => new Response
        {
            Message = message,
            AdditionalInformation = await TakeAdditionalInfoByMessage(message) ?? ""
        };

    private async Task<string?> TakeAdditionalInfoByMessage(string message)
    {
        return message switch
        {
            AccountConstants.DriverIsExist => AccountConstants.DriverIsExistAdditionalInfo,
            AccountConstants.DriverWasAdded => AccountConstants.DriverWasAddedAdditionalInfo,
            AccountConstants.DriverIsNotExist => AccountConstants.DriverIsNotExistAdditionalInfo,
            AccountConstants.DriverWasDeleted => AccountConstants.DriverWasDeletedAdditionalInfo
            ,
            _ => ""
        };
    }
}