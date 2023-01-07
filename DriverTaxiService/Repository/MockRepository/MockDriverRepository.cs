using System.Runtime.CompilerServices;
using CustomerTaxiService.Repository.Interfaces;
using DriverTaxiService.Constants;
using DriverTaxiService.Repository.Interfaces;
using Entities.General.RideData;

namespace DriverTaxiService.Repository.MockRepository;

public class MockDriveRepository : IDriveRepository
{
    private readonly IRideRepository _rideRepository;
    private readonly IAccountRepository _accountRepository;

    public MockDriveRepository(IRideRepository rideRepository, IAccountRepository accountRepository)
    {
        _rideRepository = rideRepository;
        _accountRepository = accountRepository;
    }

    public async Task<string> StartWork(string phoneNumber)
    {
        var riderEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        if (riderEntity == null)
            return AccountConstants.DriverNotExist;
        riderEntity.IsWorking = true;
        return await _accountRepository.UpdateDriver(riderEntity, phoneNumber);
    }

    public async Task<string> EndWork(string phoneNumber)
    {
        var riderEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        if (riderEntity == null)
            return AccountConstants.DriverNotExist;
        riderEntity.IsWorking = false;
        return await _accountRepository.UpdateDriver(riderEntity, phoneNumber);
    }

    public async Task<List<RideDb>> GetAllAvailableOrders(string phoneNumber)
    {
        return await _rideRepository.GetAllRides();
    }

    public async Task<string> TakeOrderById(int rideId, string phoneNumber)
    {
        return await _rideRepository.TakeRideById(rideId, phoneNumber);
    }

    public async Task<string> EndOrder(string phoneNumber)
    {
        return await _rideRepository.EndRide(phoneNumber);
    }
}