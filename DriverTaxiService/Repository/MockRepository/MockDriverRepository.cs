using Database.MockDatabase;
using DriverTaxiService.Constants.DriverConstants;
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
        return MockDatabases.RideList;
    }

    public async Task<string> TakeOrderById(int rideId, string phoneNumber)
    {
        var rideEntity = MockDatabases.RideList.FirstOrDefault(x => x.Id == rideId);
        rideEntity.IsTaken = true;
        rideEntity.DriverPhoneNumber = phoneNumber;
        rideEntity.StartTime = DateTime.Now;
        MockDatabases.RideList
            .Remove(MockDatabases.RideList
                .FirstOrDefault(x => x.CustomerPhoneNumber == rideEntity.CustomerPhoneNumber)!);
        MockDatabases.RideList.Add(rideEntity);
        return DriverConstants.Ok;
    }

    public async Task<string> EndOrder(string phoneNumber)
    {
        var rideEntity = MockDatabases.RideList
            .FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber);
        rideEntity.EndTime = DateTime.Now;
        rideEntity.IsEnd = true;
        MockDatabases.RideList
            .Remove(MockDatabases.RideList
                .FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber)!);
        MockDatabases.RideList.Add(rideEntity);
        return DriverConstants.Ok;
    }
}