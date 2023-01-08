using Database.MockDatabase;
using DriverTaxiService.Constants;
using DriverTaxiService.Repository.Interfaces;
using Entities.General.RideData;

namespace DriverTaxiService.Repository.MockRepository;

public class MockDriveRepository : IDriveRepository
{
    private readonly IAccountRepository _accountRepository;

    public MockDriveRepository(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<string> StartWork(string phoneNumber)
    {
        var riderEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        riderEntity.IsWorking = true;
        return await _accountRepository.UpdateDriver(riderEntity, phoneNumber);
    }

    public async Task<string> EndWork(string phoneNumber)
    {
        var riderEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        riderEntity.IsWorking = false;
        return await _accountRepository.UpdateDriver(riderEntity, phoneNumber);
    }

    public async Task<List<RideDb>> GetAllAvailableOrders(string phoneNumber)
    {
        return MockRideDatabase.RideList;
    }

    public async Task<string> TakeOrderById(int rideId, string phoneNumber)
    {
        var rideEntity = MockRideDatabase.RideList.FirstOrDefault(x => x.Id == rideId);
        rideEntity.IsTaken = true;
        rideEntity.DriverPhoneNumber = phoneNumber;
        rideEntity.StartTime = DateTime.Now;
        MockRideDatabase.RideList
            .Remove(MockRideDatabase.RideList
                .FirstOrDefault(x => x.CustomerPhoneNumber == rideEntity.CustomerPhoneNumber)!);
        MockRideDatabase.RideList.Add(rideEntity);
        return "Ok";
    }

    public async Task<string> EndOrder(string phoneNumber)
    {
        var rideEntity = MockRideDatabase.RideList
            .FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber);
        rideEntity.EndTime = DateTime.Now;
        rideEntity.IsEnd = true;
        MockRideDatabase.RideList
            .Remove(MockRideDatabase.RideList
                .FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber)!);
        MockRideDatabase.RideList.Add(rideEntity);
        return "Ok";
    }
}