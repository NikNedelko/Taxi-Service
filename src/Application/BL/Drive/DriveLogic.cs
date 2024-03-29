using Application.BL.General;
using DAL.Interfaces.Drive;
using DAL.Repository.Interfaces.DriverRepository;
using Domain.Entities.DriveData;
using Domain.Entities.DriverData;
using Domain.Entities.General;
using Domain.Entities.RideData;
using TaxiService.Constants.DriverConstants;

namespace Application.BL.Drive;

public class DriveLogic : IDriveLogic
{
    private readonly IDriveRepository _driveRepository;
    private readonly IDriverAccountRepository _accountRepository;
    private readonly GeneralMethods _generalMethods;

    public DriveLogic(IDriveRepository driveRepository, IDriverAccountRepository accountRepository,
        GeneralMethods generalMethods)
    {
        _driveRepository = driveRepository;
        _accountRepository = accountRepository;
        _generalMethods = generalMethods;
    }

    public async Task<Response> StartWork(string phoneNumber)
    {
        var checkExistence = await CheckDriverForExistence(phoneNumber);
        if (checkExistence == DriverConstants.DriverIsNotExist)
            return await _generalMethods.CreateResponse(DriverConstants.DriverIsNotExist);

        var checkDriverIsWorkingNow = await CheckIsDriverWorkNow(phoneNumber);
        if (checkDriverIsWorkingNow == DriverConstants.DriverIsAlreadyWorking)
            return await _generalMethods.CreateResponse(DriverConstants.DriverIsAlreadyWorking);

        return await _generalMethods.CreateResponse(await _driveRepository.StartWork(phoneNumber));
    }

    public async Task<Response> EndWork(string phoneNumber)
    {
        var checkExistence = await CheckDriverForExistence(phoneNumber);
        if (checkExistence == DriverConstants.DriverIsNotExist)
            return await _generalMethods.CreateResponse(DriverConstants.DriverIsNotExist);

        if (await CheckIsDriverWorkNow(phoneNumber) == DriverConstants.DriverIsNotWorking)
            return await _generalMethods.CreateResponse(DriverConstants.DriverIsNotWorking);

        var allRides = await GetAllAvailableOrders(phoneNumber);
        var ride = allRides.FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber);
        if (ride is null)
            return await _generalMethods.CreateResponse(await _driveRepository.EndWork(phoneNumber));

        if (!ride.IsEnd)
            return await _generalMethods.CreateResponse(DriverConstants.CanNotEndWorkWhileInRide);

        return await _generalMethods.CreateResponse(await _driveRepository.EndWork(phoneNumber));
    }

    private async Task<string> CheckDriverForExistence(string phoneNumber)
    {
        var entity = await _accountRepository.GetDriverByNumber(phoneNumber);
        return entity == null ? DriverConstants.DriverIsNotExist : DriverConstants.DriverIsExist;
    }

    private async Task<string> CheckIsDriverWorkNow(string phoneNumber)
    {
        var entity = await _accountRepository.GetDriverByNumber(phoneNumber);
        if (entity is null)
            return DriverConstants.DriverIsNotExist;

        return entity.IsWorking ? DriverConstants.DriverIsAlreadyWorking : DriverConstants.DriverIsNotWorking;
    }

    public async Task<List<RideDb>> GetAllAvailableOrders(string phoneNumber)
    {
        var riderEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        return await _driveRepository.GetAllAvailableOrders(riderEntity?.DriveClass ?? DriveClass.NoData);
    }

    public async Task<Response> TakeOrderById(int rideId, string phoneNumber)
    {
        var driverEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        if (driverEntity == null)
            return await _generalMethods.CreateResponse(DriverConstants.DriverIsNotExist);

        if (!driverEntity.IsWorking)
            return await _generalMethods.CreateResponse(DriverConstants.DriverIsNotWorking);

        var rideEntities = await GetAllAvailableOrders(phoneNumber);
        var rideEntity = rideEntities.FirstOrDefault(x => x.Id == rideId);
        if (rideEntity == null)
            return await _generalMethods.CreateResponse(DriverConstants.OrderByIdIsNotExist);

        if (rideEntity.IsTaken)
            return await _generalMethods.CreateResponse(DriverConstants.OrderIsAlreadyTaken);

        return await _generalMethods.CreateResponse(await _driveRepository.TakeOrderById(rideId, phoneNumber));
    }

    public async Task<Response> EndOrder(string phoneNumber)
    {
        var rideEntities = await GetAllAvailableOrders(phoneNumber);
        var rideEntity = rideEntities.FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber);

        if (rideEntity == null)
            return await _generalMethods.CreateResponse(DriverConstants.OrderByNumberIsNotExist);

        return await _generalMethods.CreateResponse(await _driveRepository.EndOrder(phoneNumber));
    }
}