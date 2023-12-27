using DAL.Mock.MockDatabase;
using DAL.Repository.Interfaces.DriverRepository;
using Domain.Entities.DriveData;
using Domain.Entities.DriverData;
using Domain.Entities.RideData;
using TaxiService.Constants.DriverConstants;

namespace DAL.Mock.MockRepository;

public class MockDriveRepository : IDriveRepository
{
    private readonly IDriverAccountRepository _accountRepository;

    public MockDriveRepository(IDriverAccountRepository accountRepository)
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

    public async Task<List<RideDb>> GetAllAvailableOrders(DriveClass driveClass)
    {
        return MockDatabases.RideList.Where(x => x.DriveClass == driveClass).ToList();
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

        var riderEntity = await _accountRepository.GetDriverByNumber(phoneNumber);
        riderEntity.Balance += rideEntity.Price;
        _ = await _accountRepository.UpdateDriver(riderEntity, phoneNumber);

        return DriverConstants.Ok;
    }
}