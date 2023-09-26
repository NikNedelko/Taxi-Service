using Application.BL.DriverLogic.Interface;
using Application.BL.General;
using DAL.Repository.DriverRepository.Interfaces;
using Domain.Entities.DriverApi.DriverData;
using Domain.Entities.General;
using Domain.Entities.General.RideData;
using TaxiService.Constants.DriverConstants;

namespace Application.BL.DriverLogic;

public class DriveLogic : IDriveLogic
{
    private readonly IDriveRepository _driveRepository;
    private readonly IDriverAccountRepository _accountRepository;

    public DriveLogic(IDriveRepository driveRepository, IDriverAccountRepository accountRepository)
    {
        _driveRepository = driveRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Response> StartWork(string phoneNumber)
    {
        var checkExistence = await CheckDriverForExistence(phoneNumber);
        if (checkExistence == DriverConstants.DriverIsNotExist)
            return await GeneralMethods.CreateResponse(DriverConstants.DriverIsNotExist);

        var checkDriverIsWorkingNow = await CheckIsDriverWorkNow(phoneNumber);
        if (checkDriverIsWorkingNow == DriverConstants.DriverIsAlreadyWorking)
            return await GeneralMethods.CreateResponse(DriverConstants.DriverIsAlreadyWorking);

        return await GeneralMethods.CreateResponse(await _driveRepository.StartWork(phoneNumber));
    }

    public async Task<Response> EndWork(string phoneNumber)
    {
        var checkExistence = await CheckDriverForExistence(phoneNumber);
        if (checkExistence == DriverConstants.DriverIsNotExist)
            return await GeneralMethods.CreateResponse(DriverConstants.DriverIsNotExist);

        if (await CheckIsDriverWorkNow(phoneNumber) == DriverConstants.DriverIsNotWorking)
            return await GeneralMethods.CreateResponse(DriverConstants.DriverIsNotWorking);

        var allRides = await GetAllAvailableOrders(phoneNumber);
        var ride = allRides.FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber);
        if (ride != null)
            if (!ride.IsEnd)
                return await GeneralMethods.CreateResponse(DriverConstants.CanNotEndWorkWhileInRide);

        return await GeneralMethods.CreateResponse(await _driveRepository.EndWork(phoneNumber));
    }

    private async Task<string> CheckDriverForExistence(string phoneNumber)
    {
        var entity = await _accountRepository.GetDriverByNumber(phoneNumber);
        return entity == null ? DriverConstants.DriverIsNotExist : DriverConstants.DriverIsExist;
    }

    private async Task<string> CheckIsDriverWorkNow(string phoneNumber)
    {
        var entity = await _accountRepository.GetDriverByNumber(phoneNumber);
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
            return await GeneralMethods.CreateResponse(DriverConstants.DriverIsNotExist);

        if (!driverEntity.IsWorking)
            return await GeneralMethods.CreateResponse(DriverConstants.DriverIsNotWorking);

        var rideEntities = await GetAllAvailableOrders(phoneNumber);
        var rideEntity = rideEntities.FirstOrDefault(x => x.Id == rideId);
        if (rideEntity == null)
            return await GeneralMethods.CreateResponse(DriverConstants.OrderByIdIsNotExist);

        if (rideEntity.IsTaken)
            return await GeneralMethods.CreateResponse(DriverConstants.OrderIsAlreadyTaken);

        return await GeneralMethods.CreateResponse(await _driveRepository.TakeOrderById(rideId, phoneNumber));
    }

    public async Task<Response> EndOrder(string phoneNumber)
    {
        var rideEntities = await GetAllAvailableOrders(phoneNumber);
        var rideEntity = rideEntities.FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber);

        if (rideEntity == null)
            return await GeneralMethods.CreateResponse(DriverConstants.OrderByNumberIsNotExist);

        return await GeneralMethods.CreateResponse(await _driveRepository.EndOrder(phoneNumber));
    }
}