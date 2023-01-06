using DriverTaxiService.BusinessLogic.Interface;
using DriverTaxiService.Constants;
using DriverTaxiService.Repository.Interfaces;
using Entities.General;

namespace DriverTaxiService.BusinessLogic;

public class DriveLogic : IDriveLogic
{
    private readonly IDriveRepository _driveRepository;
    private readonly IAccountRepository _accountRepository;

    public DriveLogic(IDriveRepository driveRepository, IAccountRepository accountRepository)
    {
        _driveRepository = driveRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Response> StartWork(string phoneNumber)
    {
        var checkExistence = await CheckDriverForExistence(phoneNumber);
        if (checkExistence == AccountConstants.DriverNotExist)
            return await CreateResponse(AccountConstants.DriverNotExist);
        
        var checkDriverIsWorkingNow = await CheckIsDriverWorkNow(phoneNumber);
        if (checkDriverIsWorkingNow == AccountConstants.DriverIsAlreadyWorking)
            return await CreateResponse(AccountConstants.DriverIsAlreadyWorking);

        return await CreateResponse( await _driveRepository.StartWork(phoneNumber));
    }

    public async Task<Response> EndWork(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> GetAllAvailableOrders(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> TakeOrderById(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    private async Task<string> CheckDriverForExistence(string phoneNumber)
    {
        var entity = await _accountRepository.GetDriverByNumber(phoneNumber);
        return entity == null ? AccountConstants.DriverNotExist : AccountConstants.DriverIsExist;
    }
    
    private async Task<string> CheckIsDriverWorkNow(string phoneNumber)
    {
        var entity = await _accountRepository.GetDriverByNumber(phoneNumber);
        return entity.IsWorking? AccountConstants.DriverIsAlreadyWorking : AccountConstants.DriverIsNotWorking;
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
        };
    }
}