using DriverTaxiService.BusinessLogic.Interface;
using DriverTaxiService.Constants;
using DriverTaxiService.Repository.Interfaces;
using Entities.General;
using Entities.General.RideData;

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

        return await CreateResponse(await _driveRepository.StartWork(phoneNumber));
    }

    public async Task<Response> EndWork(string phoneNumber)
    {
        var checkExistence = await CheckDriverForExistence(phoneNumber);
        if (checkExistence == AccountConstants.DriverNotExist)
            return await CreateResponse(AccountConstants.DriverNotExist);

        if (await CheckIsDriverWorkNow(phoneNumber) == AccountConstants.DriverIsNotWorking)
            return await CreateResponse("Driver is already not working");

        var allRides = await GetAllAvailableOrders(phoneNumber);
        var ride = allRides.FirstOrDefault(x=>x.DriverPhoneNumber == phoneNumber);
        if (ride != null)
            if(!ride.IsEnd)
                return await CreateResponse("You can not delete account while you in a ride");
        
        return await CreateResponse(await _driveRepository.EndWork(phoneNumber));
    }

    private async Task<string> CheckDriverForExistence(string phoneNumber)
    {
        var entity = await _accountRepository.GetDriverByNumber(phoneNumber);
        return entity == null ? AccountConstants.DriverNotExist : AccountConstants.DriverIsExist;
    }

    private async Task<string> CheckIsDriverWorkNow(string phoneNumber)
    {
        var entity = await _accountRepository.GetDriverByNumber(phoneNumber);
        return entity.IsWorking ? AccountConstants.DriverIsAlreadyWorking : AccountConstants.DriverIsNotWorking;
    }

    public async Task<List<RideDb>> GetAllAvailableOrders(string phoneNumber)
    {
        return await _driveRepository.GetAllAvailableOrders(phoneNumber);
    }

    public async Task<Response> TakeOrderById(int rideId, string phoneNumber)
    {
        var driverEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        if (driverEntity == null)
            return await CreateResponse(AccountConstants.DriverNotExist);

        if (!driverEntity.IsWorking)
            return await CreateResponse("Driver is not working");

        var rideEntities = await GetAllAvailableOrders(phoneNumber);
        var rideEntity = rideEntities.FirstOrDefault(x => x.Id == rideId);
        if (rideEntity == null)
            return await CreateResponse("Order with this id is not exist");

        if (rideEntity.IsTaken)
            return await CreateResponse("Is already taken");

        return await CreateResponse(await _driveRepository.TakeOrderById(rideId, phoneNumber));
    }

    public async Task<Response> EndOrder(string phoneNumber)
    {
        var rideEntities = await GetAllAvailableOrders(phoneNumber);
        var rideEntity = rideEntities.FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber);

        if (rideEntity == null)
            return await CreateResponse("Order with this id is not exist");

        return await CreateResponse(await _driveRepository.EndOrder(phoneNumber));
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
            _ => ""
        };
    }
}