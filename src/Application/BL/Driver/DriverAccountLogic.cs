using Application.BL.General;
using DAL.Interfaces.Driver;
using DAL.Repository.DriverRepository.Interfaces;
using Domain.Entities.DriverData;
using Domain.Entities.General;
using TaxiService.Constants.DriverConstants;

namespace Application.BL.DriverLogic;

public class DriverAccountLogic : IDriverAccountLogic
{
    private readonly IDriverAccountRepository _accountRepository;
    private readonly GeneralMethods _generalMethods;

    public DriverAccountLogic(IDriverAccountRepository accountRepository, GeneralMethods generalMethods)
    {
        _accountRepository = accountRepository;
        _generalMethods = generalMethods;
    }

    public async Task<Response> AddNewDriver(RegistrationForDriver registrationDriver)
    {
        var checkNumber = await CheckDriverByPhoneNumber(registrationDriver.PhoneNumber);
        if (checkNumber == DriverConstants.DriverIsExist)
            return await _generalMethods.CreateResponse(DriverConstants.DriverIsExist);

        var checkLicense = await CheckDriverByLicenseNumber(registrationDriver.DriverLicenseNumber);
        if (checkLicense == DriverConstants.Ok)
            return await _generalMethods.CreateResponse(DriverConstants.DriverIsExist);

        var addResult = await AddNewDriverToDatabase(registrationDriver);
        return await _generalMethods.CreateResponse(addResult);
    }

    public async Task<Response> DeleteDriver(string phoneNumber)
    {
        var checkNumber = await CheckDriverByPhoneNumber(phoneNumber);
        if (checkNumber == DriverConstants.DriverIsNotExist)
            return await _generalMethods.CreateResponse(DriverConstants.DriverIsExist);

        return await _generalMethods.CreateResponse(await _accountRepository.DeleteDriver(phoneNumber));
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